using System;
using UnityEngine;

public class MovementFirstCar : MonoBehaviour {

    public GameObject canvasFirst, canvasSecond, secondCar;

    private CarController _carController;

    private bool _firstTap;

    private void Start() {
        _carController = GetComponent<CarController>();
    }

    private void Update() {
        if (transform.position.x < 8 && !_firstTap) {
            _carController.speed = 0;
            canvasFirst.SetActive(true);
            _firstTap = true;
        }
    }

    private void OnMouseDown() {
        if (transform.position.x > 8.1f || !_firstTap)
            return;
        _carController.speed = 15f;
        canvasFirst.SetActive(false);
        secondCar.GetComponent<CarController>().speed = 10f;
        canvasSecond.SetActive(true);
    }
}