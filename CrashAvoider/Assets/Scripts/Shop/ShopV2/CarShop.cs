using UnityEngine;
using UnityEngine.UI;

public class CarShop : MonoBehaviour
{
   [SerializeField] private Button Equip;
   [SerializeField] private Button Buy;
   [SerializeField] private Text textBuy;

    private int SelectedNum, SelectedPrice;
    private ButtonSkin ButtonSkin;

   public void ShowBuy(int price, int index, ButtonSkin buttonSkin)
   {
        ButtonSkin = buttonSkin;
        textBuy.text = $"Cost: {price}";
        Equip.gameObject.SetActive(false); Buy.gameObject.SetActive(true);
        Select(index,price);
   }

    public void ShowEquip(ButtonSkin buttonSkin, int index = 0)
    {
        ButtonSkin = buttonSkin;
        Equip.gameObject.SetActive(true); Buy.gameObject.SetActive(false);
        Select(index);

    }

    public void BuyCar()
    {
     if(CountCoins.Coin >= SelectedPrice)
        {
            CountCoins.Coin = CountCoins.Coin-SelectedPrice;
            CountCoins.Save();

            ButtonSkin.isBuy = true;
            Debug.Log(ButtonSkin.name);
            EquipCar();
        }
    }

    public void EquipCar()
    {
        ButtonSkin.isEquipped = true;
        HetchBackSetSkin.skinNumber = SelectedNum;
    }

    private void Select(int num, int price = 0)
    {
        SelectedNum = num;
        SelectedPrice = price;
    }
}
