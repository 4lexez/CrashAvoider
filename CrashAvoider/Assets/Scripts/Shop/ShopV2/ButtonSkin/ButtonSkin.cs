using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonSkin : MonoBehaviour
{
    public int number;

    [SerializeField] protected int price;
    public int Price => price;
    [SerializeField] protected string hash;

    [SerializeField] protected CarShop carShop;

    [HideInInspector] public bool isBuy;
    [HideInInspector] public bool isEquipped;

    public void Save(int skin)
    {
        if (isBuy) PlayerPrefs.SetInt("isBuy" + hash, 1);
        if (skin == number) PlayerPrefs.SetInt(hash, 1);
        else PlayerPrefs.SetInt(hash, 0);
        
    }

    public void Load()
    {
        isEquipped = IntToBoolean(PlayerPrefs.GetInt(hash));
        isBuy = IntToBoolean(PlayerPrefs.GetInt("isBuy" + hash));
    }

    public void Buy()
    {
        isBuy = true;
        PlayerPrefs.SetInt("isBuy" + hash, 1);
    }

    public abstract void SelectCar();

    public abstract void OnDestroy();

    private void Awake()
    {
        Load();
        if (isEquipped) gameObject.GetComponent<Button>().onClick.Invoke();
    }

    public void Select()
    {
        if (isBuy) 
        {
            carShop.ShowEquip(this);
            carShop.EquipCar();
        }
        else
        {
            carShop.ShowBuy(price, this);
        }
    }
    
    private bool IntToBoolean(int integer)
    {
        if (integer == 1) return true;
        else return false;
    }
}
