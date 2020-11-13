using System;
using UnityEngine;

public class IsNoAds : MonoBehaviour {
    private void Start() {
        if (PlayerPrefs.GetString("NoAds") == "yes")
            Destroy(gameObject);
    }
    
}
