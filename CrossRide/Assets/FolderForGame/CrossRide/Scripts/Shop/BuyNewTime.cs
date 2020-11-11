using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyNewTime : MonoBehaviour
{
    public AudioClip success, fail;
    public Animation coinsText;
    public Text coinsCount;
    public Text timeCount;
    [NonSerialized] public int TimeCount;

    private void Start()
    {
        timeCount.text = PlayerPrefs.GetInt("Time", TimeCount).ToString();
    }

    public void BuyNewTimeCount(int needCoins)
    {
        int coins = PlayerPrefs.GetInt("Coins");
        if (coins < needCoins)
        {
            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = fail;
                GetComponent<AudioSource>().Play();
            }
            coinsText.Play();
        }
        else
        {
            // Buy time
            switch (needCoins)
            {
                case 50:
                    TimeCount++;
                    PlayerPrefs.SetInt("Time", TimeCount);
                    timeCount.text = PlayerPrefs.GetInt("Time", TimeCount).ToString();
                    break;
            }

            int nowCoins = coins - needCoins;
            coinsCount.text = nowCoins.ToString();
            PlayerPrefs.SetInt("Coins", nowCoins);

            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = success;
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
