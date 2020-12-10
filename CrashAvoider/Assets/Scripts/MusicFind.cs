using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable] 
public class MusicFind
{
    public static GameObject Music;
    private void Awake()
    {
        Music = GameObject.Find("Music").gameObject;
    }
}
