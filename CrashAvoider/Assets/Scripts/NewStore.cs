using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewStore : MonoBehaviour
{

    [SerializeField]private Text Coins;
    [SerializeField] private bool TestCount, DeleteAllChanges;
    [SerializeField] private string count;
    private int id, price, coins;
    [SerializeField] private Text[] txtSelector;
    private string select = "Select", selected = "Selected";

    void Start()
    {
        if(DeleteAllChanges) DeleteAll();
        Coins = GameObject.Find("Coins").transform.GetChild(0).GetComponent<Text>();
        if (!TestCount) Coins.text = PlayerPrefs.GetInt("Money").ToString();
        else Coins.text = count;
        CheckPrefs();
        print(PlayerPrefs.GetInt("Money"));
    }
    public void BtnBuySkin(int skinPrice)
    {
        int coin = int.Parse(Coins.text);

            switch (skinPrice)
            {
                case 0:
                    id = 0;
                    break;
                case 500:
                    id = 1;
                    break;
                case 1000:
                    id = 2;
                    break;
                case 1500:
                    id = 3;
                    break;
                case 2300:
                    id = 4;
                    break;
                case 3100:
                    id = 5;
                    break;
                case 3500:
                    id = 6;
                    break;
                case 4100:
                    id = 7;
                    break;
                case 5000:
                    id = 8;
                    break;
                case 10000:
                    id = 9;
                    break;
                case 25000:
                    id = 10;
                    break;
            }

        //CheckPrefs();
        if (coin >= skinPrice || PlayerPrefs.GetInt($"isSelected_{id}") == 1)
        {
            price = skinPrice;
            coins = coin;
            BtnSelector(id);
        }
    }
    
    
    public void BtnSelector(int skinId)
    {
        PlayerPrefs.SetInt("SkinId", skinId);
        print(skinId);
        IsItSelected(skinId);
    }
    public void IsItSelected(int skinId)
    {
        for (int i = 0; i < txtSelector.Length; i++)
        {
            if (i == PlayerPrefs.GetInt("SkinId"))
            {
                SetCoin(price, coins, skinId);
                PlayerPrefs.SetInt($"isSelected_{skinId}", 1);
                txtSelector[skinId].text = selected;
                print("isItSelected");
            }
            else if(PlayerPrefs.GetInt($"isSelected_{i}") == 1)
            {
                txtSelector[i].text = select;
            }
        }
    }
    void CheckPrefs()
    {
        for (int i = 0; i < txtSelector.Length; i++)
        {
            if (i == PlayerPrefs.GetInt("SkinId"))
            {
                txtSelector[i].text = selected;
            }
            else if(PlayerPrefs.GetInt($"isSelected_{i}") == 1)
            {
                txtSelector[i].text = select;
            }
        }
    }
    void DeleteAll()
    {
        PlayerPrefs.SetInt("SkinId", 0);
        for (int i = 0; i < txtSelector.Length; i++)
        {
         PlayerPrefs.DeleteKey($"isSelected_{i}");
        }
    }
    void SetCoin(int price, int coin, int id)
    {
        if (PlayerPrefs.GetInt($"isSelected_{id}") == 1) return;
        int nowCoins = coin - price;
        Coins.text = nowCoins.ToString();
        PlayerPrefs.SetInt("Money", nowCoins);
        print(price + " " +  coin);
    }
}
