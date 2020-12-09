using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class MusicObject : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] ListOfMusic;
    //[SerializeField] private AudioMixer mixer;
    private bool IsSpawned;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ListOfMusic[Random.Range(0, ListOfMusic.Length)];
        GetComponent<AudioSource>().Play();
        if (PlayerPrefs.GetString("music") == "No")
        {
            audioSource.mute = true;
        }

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



