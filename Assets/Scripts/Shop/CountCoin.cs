using System;
using UnityEngine;
using UnityEngine.UI;

public class CountCoin : MonoBehaviour {
    private void Start() {
        int coins = PlayerPrefs.GetInt("Coins");
        GetComponent<Text>().text = PlayerPrefs.GetInt("Coins").ToString();
    }
}