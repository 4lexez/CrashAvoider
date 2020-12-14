using UnityEngine.UI;
using UnityEngine;

public class CountCoins : MonoBehaviour
{
    [SerializeField] private Text thisT;
    public static int Coin;
    private const string hash = "SettingWindowSizenbm,f35gsdnv3";

    // private const int hashInt = 42819;

    private void Awake()
    {
        Coin = PlayerPrefs.GetInt(hash);
        thisT.text = $"{Coin}";
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(hash, Coin);
    }
}