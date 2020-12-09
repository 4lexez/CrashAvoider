using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable 0649
public class BuyNewTime : MonoBehaviour
{
    [SerializeField] private AudioClip success, fail;
    [SerializeField] private Animation coinsText;
    [SerializeField] private Text coinsCount;
    [SerializeField] private Text timeCount;
    private int TimeCount;

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
                    PlayerPrefs.SetInt("Time", 1 + PlayerPrefs.GetInt("Time"));
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
