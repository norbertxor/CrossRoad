using System;
using UnityEngine;
using UnityEngine.UI;

public class ChekMaps : MonoBehaviour {

    public Image[] maps;
    public Sprite selected, notSelected;
    
    private BuyMapCoins _buyMapCoins;

    private void Start() {
        WhichMapSelected();
        _buyMapCoins = GetComponent<BuyMapCoins>();
        if (PlayerPrefs.GetString("City") == "Open") {
            _buyMapCoins.coins1000.SetActive(false);
            _buyMapCoins.money0_99.SetActive(false);
            _buyMapCoins.cityButton.SetActive(true);
        }

        if (PlayerPrefs.GetString("Megapolis") == "Open") {
            _buyMapCoins.coins5000.SetActive(false);
            _buyMapCoins.money1_99.SetActive(false);
            _buyMapCoins.megapolisButton.SetActive(true);
        }
    }

    public void WhichMapSelected() {
        switch (PlayerPrefs.GetInt("NowMap")) {
            case 2:
                maps[0].sprite = notSelected;
                maps[1].sprite = selected;
                maps[2].sprite = notSelected;
                break;
            case 3:
                maps[0].sprite = notSelected;
                maps[1].sprite = notSelected;
                maps[2].sprite = selected;
                break;
            default:
                maps[0].sprite = selected;
                maps[1].sprite = notSelected;
                maps[2].sprite = notSelected;
                break;
        }
    }
}