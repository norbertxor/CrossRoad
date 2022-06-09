using UnityEngine;
using UnityEngine.UI;


public class BuyMapCoins : MonoBehaviour {

    public GameObject coins1000, coins5000, money0_99, money1_99, cityButton, megapolisButton;
    public Animation coinsTextAnimation;
    public Text coinsCount;

    public void BuyNewMap(int needCoins) {
        int coins = PlayerPrefs.GetInt("Coins");
        if (coins < needCoins)
            coinsTextAnimation.Play();
        else {
            switch (needCoins) {
                case 1000:
                    PlayerPrefs.SetString("City", "Open");
                    PlayerPrefs.SetInt("NowMap", 2);
                    GetComponent<ChekMaps>().WhichMapSelected();
                    coins1000.SetActive(false);
                    money0_99.SetActive(false);
                    cityButton.SetActive(true);
                    break;
                case 5000:
                    PlayerPrefs.SetString("Megapolis", "Open");
                    PlayerPrefs.SetInt("NowMap", 3);
                    GetComponent<ChekMaps>().WhichMapSelected();
                    coins5000.SetActive(false);
                    money1_99.SetActive(false);
                    megapolisButton.SetActive(true);
                    break;
            }

            int nowCoins = coins - needCoins;
            coinsCount.text = nowCoins.ToString();
            PlayerPrefs.SetInt("Coins", nowCoins);
        }
    }
}