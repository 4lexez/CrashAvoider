using UnityEngine;

public class ChooseMap : MonoBehaviour {

    public AudioClip btnClick;
    
    public void ChooseNewMap(int numberMap) {
        if (PlayerPrefs.GetString("music") != "No") {
            GetComponent<AudioSource>().clip = btnClick;
            GetComponent<AudioSource>().Play();
        }

        PlayerPrefs.SetInt("NowMap", numberMap);
        GetComponent<CheckMaps>().whichMapSelected();
    }
    
}
