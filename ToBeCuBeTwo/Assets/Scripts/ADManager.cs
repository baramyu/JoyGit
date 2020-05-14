using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ADManager : MonoBehaviour
{
    private BannerView bannerView;

    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-4275773062745910~7543728465";
#elif UNITY_IPHONE
        string appId = "unexpected_platform";
#else
        string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        //testAdUnitId : ca-app-pub-3940256099942544/6300978111
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        string adUnitId = "unexpected_platform";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

        bannerView.Destroy();
    }

}