using UnityEngine;

public class DiliveryButtonSkin : ButtonSkin
{
    public override void SelectCar()=> DeliverySetSkin.skinNumber = number;
    public override void OnDestroy()=> Save(DeliverySetSkin.skinNumber);
}
