using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {

    public GameObject[] maps;
    public GameObject[] cars;
    public GameObject canvasLoosePanel;
    public Text nowScore, bestScore, coinsCount;
    public float minSpawnTime = 4f, maxSpawnTime = 8f;
    public bool isMainScene;
    
    private Coroutine _bottomCoroutine,_rightCoroutine,_leftCoroutine,_topCoroutine;
    private bool _isNowLoose;
    private int _countCars = 0;

    
    private void Start() {
       if (PlayerPrefs.GetInt("NowMap") == 2) {
            maps[1].SetActive(true);
            Destroy(maps[0]);
            Destroy(maps[2]);
       } else if (PlayerPrefs.GetInt("NowMap") == 3) {
            maps[2].SetActive(true);
            Destroy(maps[1]);
            Destroy(maps[0]);
       } else {
           maps[0].SetActive(true);
           Destroy(maps[1]);
           Destroy(maps[2]);
       }

       CarController.isLoose = false;
        CarController.scoreCount = 0;
        if (isMainScene) {
            minSpawnTime = 7f;
            maxSpawnTime = 10f;
        }
        _bottomCoroutine = StartCoroutine(BottomCars());
        _rightCoroutine = StartCoroutine(RightCars());
        _leftCoroutine = StartCoroutine(LeftCars());
        _topCoroutine = StartCoroutine(TopCars());
    }

    private void Update() {
        //for debug
        if (Input.GetKeyUp(KeyCode.R)) {
            // PlayerPrefs.SetString("First Game", "Yes");
            // Debug.Log("reset study");
        }


        if (CarController.isLoose && !_isNowLoose) {
            StopCoroutine(_bottomCoroutine);
            StopCoroutine(_rightCoroutine);
            StopCoroutine(_leftCoroutine);
            StopCoroutine(_topCoroutine);
            nowScore.text = "SCORE:" + CarController.scoreCount.ToString();
            if(PlayerPrefs.GetInt("Score") < CarController.scoreCount)
                PlayerPrefs.SetInt("Score", CarController.scoreCount);
            bestScore.text = "BEST SCORE:" + PlayerPrefs.GetInt("Score").ToString();
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CarController.scoreCount);
            coinsCount.text = PlayerPrefs.GetInt("Coins").ToString();
            canvasLoosePanel.SetActive(true);
            _isNowLoose = true;
        }
    }

    IEnumerator RightCars() {
        while (true) {
            SpawnCars(new Vector3(-85.2f, -0.13f, 2.7f), 270f);
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    IEnumerator BottomCars() {
        while (true) {
            SpawnCars(new Vector3(-1.1f, -0.13f, -22.33f), 180f);
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    IEnumerator LeftCars() {
        while (true) {
            SpawnCars(new Vector3(26.82f, -0.13f, 10.3f), 90f);
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    IEnumerator TopCars() {
        while (true) {
            SpawnCars(new Vector3(-6.8f, -0.13f, 62.8f), 0f);
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void SpawnCars(Vector3 pos, float rotationY) {
        GameObject obj = Instantiate(cars[Random.Range(0,cars.Length)], pos, Quaternion.Euler(0, rotationY, 0)) as GameObject;
        obj.name = "Car_" + ++_countCars;
        int random = isMainScene ? 1 : Random.Range(1,4);
        if (isMainScene)
            obj.GetComponent<CarController>().speed = 8f;
        switch (random) {
            case 1:
                //move right
                obj.GetComponent<CarController>().rightTurn = true;
                obj.GetComponent<CarController>().leftTurn = false;
                break;
            case 2:
                //move left
                obj.GetComponent<CarController>().leftTurn = true;
                obj.GetComponent<CarController>().rightTurn = false;
                break;
            case 3:
                //keep calm and moving forward
                obj.GetComponent<CarController>().rightTurn = false;
                obj.GetComponent<CarController>().leftTurn = false;
                break;
        }
    }
}