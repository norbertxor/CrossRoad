using System;
using UnityEngine;

public class MapSelector : MonoBehaviour {

    public void ChooseNewMap(int numberMap) {
        PlayerPrefs.SetInt("NowMap", numberMap);
        GetComponent<ChekMaps>().WhichMapSelected();
    }
}