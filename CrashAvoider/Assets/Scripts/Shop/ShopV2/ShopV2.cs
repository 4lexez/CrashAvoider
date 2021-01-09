using UnityEngine;

public class ShopV2 : MonoBehaviour
{
    [SerializeField] private Canvas[] panel;

    public void EnableScene(int num)
    {
        HideOtherPanel(num);
        panel[num].gameObject.SetActive(true);
        //panel[num].enabled = true;
    }
 
    private void HideOtherPanel(int num)
    {
       foreach(Canvas canvas in panel)
        {
            if(panel[num]!=canvas) canvas.gameObject.SetActive(false);
            //if (panel[num] != canvas) canvas.enabled = false;
        }
    }
}
    public enum PanelNum{ Main, Chets, Cars, Maps }
