using System;
using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour {
    
    public LayerMask carsLayer;
    public GameObject turnLeftSignal, turnRightSignal, explosion, speedUp;
    public bool rightTurn, leftTurn;
    public float speed = 15f, force = 50f;

    [NonSerialized] public static int scoreCount;
    [NonSerialized] public static bool isLoose;
    [NonSerialized] public bool carPassed = false;
    
    private Rigidbody _carRb;
    private Camera _mainCam;
    private bool _isMovingFast, _isCrashed, _isSlowed;
    private float _originRotationY, _rotationMultRight = 8f, _rotationMultLeft = 4.8f;
    

    private void Start() {
        _mainCam = Camera.main;
        _originRotationY = transform.eulerAngles.y;
        _carRb = GetComponent<Rigidbody>();
        if (rightTurn)
            StartCoroutine(TurnSignal(turnRightSignal));
        else if (leftTurn)
            StartCoroutine(TurnSignal(turnLeftSignal));
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Cars") && !_isCrashed) {
            isLoose = true;
            _isCrashed = true;
            speed = 0f;
            collision.gameObject.GetComponent<CarController>().speed = 0f;
            GameObject carExplosionVfx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(carExplosionVfx, 6f);
            if (_isMovingFast)
                force *= 1.5f;
            _carRb.AddRelativeForce(Vector3.back * force);
        }
    }

    IEnumerator TurnSignal(GameObject obj) {
        while (!carPassed) {
            obj.SetActive(!obj.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void FixedUpdate() {
        _carRb.MovePosition(transform.position - transform.forward*speed*Time.fixedDeltaTime);
    }

    private void Update() {
        if (Slowdown.isSlowed && !_isSlowed) {
            speed /= 2f;
            _isSlowed = true;
        }

        if (!Slowdown.isSlowed && _isSlowed) {
            speed *= 2f;
            _isSlowed = false;
        }
#if UNITY_EDITOR
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
#else
        if (Input.touchCount == 0)
            return;
        Ray ray = _mainCam.ScreenPointToRay(Input.GetTouch(0).position);
#endif
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,100f, carsLayer)) {
            string carName = hit.transform.gameObject.name;
#if UNITY_EDITOR            
            if (Input.GetMouseButtonDown(0) && !_isMovingFast && gameObject.name == carName) {
#else 
            if (Input.GetTouch(0).phase == TouchPhase.Began && !_isMovingFast && gameObject.name == carName) {
#endif
                GameObject carSpeedUpVfx = Instantiate(speedUp, new Vector3(transform.position.x + 1f, transform.position.y + 1f, transform.position.z), Quaternion.Euler(90,0,0)) as GameObject;
                Destroy(carSpeedUpVfx, 1f);
                speed *= 2f;
                if (_isSlowed)
                    speed *= 3;
                _isMovingFast = true;
            }

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Cars") && other.GetComponent<CarController>().carPassed) 
            other.GetComponent<CarController>().speed = speed+5f; 
    }

    private void OnTriggerStay(Collider other) {
        if(isLoose)
            return;
        if (other.transform.CompareTag("TurnBlockRight") && rightTurn)
            RotateCar(_rotationMultRight);
        else if (other.transform.CompareTag("TurnBlockLeft") && leftTurn)
             RotateCar(_rotationMultLeft,-1);
    }

    private void OnTriggerExit(Collider other) {
        if(isLoose)
            return;
        if (other.gameObject.CompareTag("SpeedCorrection")) {
            if(carPassed)
                return;
            carPassed = true;
            Collider[] coliders = GetComponents<BoxCollider>();
            foreach (Collider col in coliders)
                col.enabled = true;
            scoreCount++;
        }
        if (other.gameObject.CompareTag("TurnBlockRight") && rightTurn)
            _carRb.rotation = Quaternion.Euler(0,_originRotationY+90f,0);
        if (other.gameObject.CompareTag("TurnBlockLeft") && leftTurn)
            _carRb.rotation = Quaternion.Euler(0, _originRotationY - 90f, 0);
        if(other.gameObject.CompareTag("Deleter"))
            Destroy(this.gameObject);
    }

    private void RotateCar(float speedRotate, int direction = 1) {
        if(isLoose)
            return;;
        if (direction == -1 && transform.localRotation.eulerAngles.y < _originRotationY - 90f)
            return;
        float rotationSpeed = speed * speedRotate * direction;
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0,rotationSpeed,0) * Time.fixedDeltaTime);
        _carRb.MoveRotation(_carRb.rotation * deltaRotation);
    }
}