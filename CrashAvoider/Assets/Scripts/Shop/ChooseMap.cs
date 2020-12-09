using UnityEngine;
#pragma warning disable 0649
public class ChooseMap : MonoBehaviour {

    [SerializeField] private AudioClip btnClick;
    
    public void ChooseNewMap(int numberMap) {
        if (PlayerPrefs.GetString("music") != "No") {
            GetComponent<AudioSource>().clip = btnClick;
            GetComponent<AudioSource>().Play();
        }

        PlayerPrefs.SetInt("NowMap", numberMap);
        GetComponent<CheckMaps>().whichMapSelected();
    }
    
}
