using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public Skin[] skins;
    public Skin[] skinsDelivery;
    public Skin[] skinsLuxury;
    public Skin[] skinsSedan;

    public void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    public void PlusCoins()
    {
        CountCoins.Coin = 1200;
        CountCoins.Save();
    }
}
