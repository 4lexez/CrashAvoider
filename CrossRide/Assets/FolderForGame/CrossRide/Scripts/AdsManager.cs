using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour {
    // test id : 	ca-app-pub-3940256099942544/8691691433
    // my id : ca-app-pub-3940256099942544/1033173712
#if UNITY_EDITOR
    string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_ANDROID
    string adUnitId = "ca-app-pub-9626448578379214/8441202116";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-9626448578379214/4354617445";
#else
        string adUnitId = "unexpected_platform";
#endif
    private InterstitialAd interstitial;
    private int nowLoses;

    private void Start() {
        DontDestroyOnLoad(gameObject);
        
        DestroyAndStartNew(true);
    }

    private void Update() {
        if (interstitial.IsLoaded() && GameController.countLoses % 3 == 0 && GameController.countLoses != 0 && GameController.countLoses != nowLoses) {
            nowLoses = GameController.countLoses;
            interstitial.Show();
        }
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        DestroyAndStartNew();
    }

    public void HandleOnAdClosed(object sender, EventArgs args) {
        DestroyAndStartNew();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args) {
        DestroyAndStartNew();
    }

    void DestroyAndStartNew(bool isFirst = false) {
        if(!isFirst)
            interstitial.Destroy();
        
        interstitial = new InterstitialAd(adUnitId);
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
}
