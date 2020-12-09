using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 0649
public class BuyMapCoins : MonoBehaviour {

    [SerializeField] private AudioClip success, fail;
    public GameObject coins1000, coins5000, money0_99, money1_99, city_btn, megapolis_btn;
    [SerializeField] private Animation coinsText;
    [SerializeField] private Text coinsCount;

    public void BuyNewMap(int needCoins) {
        int coins = PlayerPrefs.GetInt("Coins"); // берем сколько у нас монет
        if (coins < needCoins) { // На кнопке висит нужная сумма, и сравниваем с текущим кол-во монет
            if (PlayerPrefs.GetString("music") != "No") {// если нет, то fail
                GetComponent<AudioSource>().clip = fail;
                GetComponent<AudioSource>().Play();
            }

            coinsText.Play();// проигрываем звуки фейла
        }
        else {
            // Buy map
            switch (needCoins) { // чекаем сколько стоит карта
                case 1000:
                    PlayerPrefs.SetString("City", "Open");// открываем карту
                    PlayerPrefs.SetInt("NowMap", 2); //ставим 2, потом gameController ставит по дочерним объектам карту
                    GetComponent<CheckMaps>().whichMapSelected();// проставляем галочки и выбраную карту
                    coins1000.SetActive(false);// откл кнопки
                    money0_99.SetActive(false);// откл покупку за реал деньги
                    city_btn.SetActive(true);// вкл возможность выбрать карту
                    break;
                case 5000:// тут такой же метод
                    PlayerPrefs.SetString("Megapolis", "Open");
                    PlayerPrefs.SetInt("NowMap", 3);
                    GetComponent<CheckMaps>().whichMapSelected();
                    coins5000.SetActive(false);
                    money1_99.SetActive(false);
                    megapolis_btn.SetActive(true);
                    break;
            }

            int nowCoins = coins - needCoins; // отнимаем деньги со счета
            coinsCount.text = nowCoins.ToString();// обновляем наш счет текст
            PlayerPrefs.SetInt("Coins", nowCoins); // ставим PlayerPrefs

            if (PlayerPrefs.GetString("music") != "No") {// проигрываем звук успешной покупки
                GetComponent<AudioSource>().clip = success;
                GetComponent<AudioSource>().Play();
            }
        }
    }
    
}
