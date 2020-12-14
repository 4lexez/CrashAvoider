using UnityEngine;
using UnityEngine.UI;

public class ButtonSkin : MonoBehaviour
{
    public int number;
    [SerializeField] private int price;
    [SerializeField] private string hash;
    [SerializeField] private CarShop carShop;

    [HideInInspector] public bool isBuy;
    [HideInInspector] public bool isEquipped;

    public void Select()        
    {
        if (isBuy)
        {
            carShop.ShowEquip(this,number);
        }
        else
        {
            carShop.ShowBuy(price, number, this);                                   
        }
    }


    private void Awake()
    {
        isEquipped = IntToBoolean(PlayerPrefs.GetInt(hash));
        isBuy = IntToBoolean(PlayerPrefs.GetInt("isBuy"+ hash));

        if(isEquipped)
        {
            gameObject.GetComponent<Button>().onClick.Invoke();
        }
    }
    public virtual void OnDestroy()
    {
        if (HetchBackSetSkin.skinNumber == number) PlayerPrefs.SetInt(hash, 1);
        else PlayerPrefs.SetInt(hash, 0);
        if(isBuy) PlayerPrefs.SetInt("isBuy" + hash, 1);
    }

    private bool IntToBoolean(int integer)    
    {
        if (integer == 1) return true;
        else return false;
    }
}
