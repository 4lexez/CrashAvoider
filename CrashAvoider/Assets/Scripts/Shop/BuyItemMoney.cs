using System;
using UnityEngine;

public class BuyItemMoney : MonoBehaviour {
    
    public enum Types {
        REMOVE_ADS, OPEN_CITY, OPEN_MEGAPOLIS
    }

    public Types type;

    public void BuyItem() {
        switch (type) {
            case Types.REMOVE_ADS:
                IAPManager.instance.BuyNoAds();
                break;
            case Types.OPEN_CITY:
                IAPManager.instance.BuyCityMap();
                break;
            case Types.OPEN_MEGAPOLIS:
                IAPManager.instance.BuyMegapolisMap();
                break;
        }
    }

}
