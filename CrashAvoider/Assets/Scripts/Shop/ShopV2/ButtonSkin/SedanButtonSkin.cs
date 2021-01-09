public class SedanButtonSkin : ButtonSkin
{
   public override void SelectCar() => SedanSetSkin.skinNumber = number;
   public override void OnDestroy() => Save(SedanSetSkin.skinNumber);
}
