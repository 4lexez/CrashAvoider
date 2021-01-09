using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuxureButtonSkin : ButtonSkin
{
   public override void OnDestroy()=> Save(LuxurySetSkin.skinNumber);
   public override void SelectCar()=> LuxurySetSkin.skinNumber = number;
   
}
