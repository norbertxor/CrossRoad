using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SecondCar : MonoBehaviour {

    private void OnDestroy() {
        PlayerPrefs.SetString("First Game", "No");
        PlayerPrefs.SetInt("Slowdown",5);
        SceneManager.LoadScene("Game");
        Fading.fadingAvaleble = false;
        
    }
}