using System;
using UnityEngine;
using UnityEngine.UI;

public class BuySlowdown : MonoBehaviour {

    public Text slowdownCounter;
    public GameObject coinsText;
    private int _availableSlowdown;

    private void Start() {
        _availableSlowdown = PlayerPrefs.GetInt("Slowdown");
        slowdownCounter.text = _availableSlowdown.ToString();
    }

    public void BuySlowdonwForCoins(int needCoins) {
        transform.localScale = new Vector3(1f,1f,1f);
        int coins = PlayerPrefs.GetInt("Coins");
        if (coins >= needCoins) {
            coins -= needCoins;
            _availableSlowdown++;
            PlayerPrefs.SetInt("Slowdown", _availableSlowdown);
            PlayerPrefs.SetInt("Coins", coins);
            slowdownCounter.text = _availableSlowdown.ToString();
            coinsText.GetComponent<Text>().text = coins.ToString();
        }
        else 
            coinsText.GetComponent<Animation>().Play();
    }
    
    public void SetPressed() {
        transform.localScale = new Vector3(1.1f,1.1f,1.1f); 
    }
    
}