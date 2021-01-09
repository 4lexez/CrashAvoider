public class HatchBackButtonSkin : ButtonSkin
{
    public override void OnDestroy()=> Save(HetchBackSetSkin.skinNumber);
    public override void SelectCar() => HetchBackSetSkin.skinNumber = number;
}
