using UnityEngine;
using UnityEngine.UI;

public class CarShop : MonoBehaviour
{
    [SerializeField] private Button Equip;
    [SerializeField] private Button Buy;
    [SerializeField] private Text textBuy;

    private int SelectedPrice;
    private ButtonSkin ButtonSkin;

    public void ShowBuy(int price, ButtonSkin buttonSkin)
    {
        textBuy.text = $"Cost: {price}";
        SetActiveUIButtons(false, true);
        Select(buttonSkin, price);
    }

    public void ShowEquip(ButtonSkin buttonSkin)
    {
        SetActiveUIButtons(true, false);
        Select(buttonSkin);
    }

    public void BuyCar()
    {
        if (CountCoins.Coin >= SelectedPrice)
        {
            CountCoins.Coin = CountCoins.Coin - SelectedPrice;
            CountCoins.Save();

            ButtonSkin.isBuy = true;
            EquipCar();
        }
    }

    public void EquipCar()
    {
        SetActiveUIButtons(true, false);
        ButtonSkin.isEquipped = true;
        ButtonSkin.SelectCar();
    }

    private void Select(ButtonSkin buttonSkin, int price = 0)
    {
        SelectedPrice = price;
        ButtonSkin = buttonSkin;
    }

    private void SetActiveUIButtons(bool equip, bool buy)
    {
        Equip.gameObject.SetActive(equip); Buy.gameObject.SetActive(buy);
    }
}
