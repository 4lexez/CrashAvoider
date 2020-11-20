using UnityEngine;
using UnityEngine.UI;

public class BuyMapCoins : MonoBehaviour {

    [SerializeField] private AudioClip success, fail;
    public GameObject coins1000, coins5000, money0_99, money1_99, city_btn, megapolis_btn;
    [SerializeField] private Animation coinsText;
    [SerializeField] private Text coinsCount;

    public void BuyNewMap(int needCoins) {
        int coins = PlayerPrefs.GetInt("Coins");
        if (coins < needCoins) {
            if (PlayerPrefs.GetString("music") != "No") {
                GetComponent<AudioSource>().clip = fail;
                GetComponent<AudioSource>().Play();
            }

            coinsText.Play();
        }
        else {
            // Buy map
            switch (needCoins) {
                case 1000:
                    PlayerPrefs.SetString("City", "Open");
                    PlayerPrefs.SetInt("NowMap", 2);
                    GetComponent<CheckMaps>().whichMapSelected();
                    coins1000.SetActive(false);
                    money0_99.SetActive(false);
                    city_btn.SetActive(true);
                    break;
                case 5000:
                    PlayerPrefs.SetString("Megapolis", "Open");
                    PlayerPrefs.SetInt("NowMap", 3);
                    GetComponent<CheckMaps>().whichMapSelected();
                    coins5000.SetActive(false);
                    money1_99.SetActive(false);
                    megapolis_btn.SetActive(true);
                    break;
            }

            int nowCoins = coins - needCoins;
            coinsCount.text = nowCoins.ToString();
            PlayerPrefs.SetInt("Coins", nowCoins);
            
            if (PlayerPrefs.GetString("music") != "No") {
                GetComponent<AudioSource>().clip = success;
                GetComponent<AudioSource>().Play();
            }
        }
    }
    
}
