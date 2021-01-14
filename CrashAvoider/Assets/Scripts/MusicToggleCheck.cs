using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggleCheck : MonoBehaviour
{
    [SerializeField] private MixerController AudioController;
    void Start()
    {
        GetComponent<Toggle>().isOn = false;
        /*MusicToggle = GetComponent<Toggle>();
        if (AudioController.VolumeValue == 0)
            MusicToggle.isOn = true;
        //GetComponent<Toggle>().isOn = true;
        else;
        MusicToggle.isOn = false;
        //GetComponent<Toggle>().isOn = false;*/
    }
}
