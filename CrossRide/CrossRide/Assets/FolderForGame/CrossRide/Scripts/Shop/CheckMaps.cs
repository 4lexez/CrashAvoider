using System;
using UnityEngine;
using UnityEngine.UI;

public class CheckMaps : MonoBehaviour {

    public Image[] maps;
    public Sprite selected, notSelected;
    private BuyMapCoins _mapCoins;

    private void Start() {
        
        whichMapSelected();
        _mapCoins = GetComponent<BuyMapCoins>();
        if (PlayerPrefs.GetString("City") == "Open") {
            _mapCoins.coins1000.SetActive(false);
            _mapCoins.money0_99.SetActive(false);
            _mapCoins.city_btn.SetActive(true);
        }
        if (PlayerPrefs.GetString("Megapolis") == "Open") {
            _mapCoins.coins5000.SetActive(false);
            _mapCoins.money1_99.SetActive(false);
            _mapCoins.megapolis_btn.SetActive(true);
        }
    }

    public void whichMapSelected() {
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
