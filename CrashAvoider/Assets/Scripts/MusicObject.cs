using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicObject : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioMixer mixer;
    private bool IsSpawned;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (PlayerPrefs.GetString("music") == "No") audioSource.mute = true;

        int numMusicPlayers = FindObjectsOfType<MusicObject>().Length;
        if (numMusicPlayers != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    public void OnSoundButtonClick()
    {
     audioSource.mute = !audioSource.mute;
        
    }
}



