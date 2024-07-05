using UnityEngine;
using System.Collections;
//using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;

public class DisplayAds : MonoBehaviour
{
    string appId = "ca-app-pub-3176152248788238~8747671915";//"ca-app-pub-4357894923588656~8418864328";
    string UnityAdId = "3179706";//"1528228";

    // string adUnitId_AdmobBanner = "ca-app-pub-3940256099942544/6300978111"; // test
    string adUnitId_AdmobBanner = "ca-app-pub-3176152248788238/6121508577";//"ca-app-pub-4357894923588656/9895597520";   

    // string adUnitId_AdmobInterstitial = "ca-app-pub-3940256099942544/1033173712"; // test																				 
    static string adUnitId_AdmobInterstitial = "ca-app-pub-3176152248788238/2061299474";//"ca-app-pub-4357894923588656/7618419935";

    //public static BannerView bannerView = null;
    //public static InterstitialAd interstitial = null;

    static int[][] maparray = new int[3][] { new int[3] { 0, 728, 90 },
                                            new int[3] { 1, 468, 60 },
                                            new int[3] { 2, 320, 50 } };
    static float Density = 1;
    static int arrayIndex = 0;

    //static AdSize currentAdsize = AdSize.SmartBanner;

    //static AdSize adSize_1 = AdSize.SmartBanner;
    //static AdSize adSize_2 = AdSize.IABBanner;
    //static AdSize adSize_3 = AdSize.Banner;


    //static AdRequest request = null;
    public static DisplayAds instance = null;

    static bool displayUnityAd = true;
    static int interstitialDisplyIndex = 10;
    static float interstitialDisplayInterval = 40;
    static float interstitialDisplayTime = 0;
    static int gamePlayIndex = 0;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaClass metricsClass = new AndroidJavaClass("android.util.DisplayMetrics");
            AndroidJavaObject metricsInstance = new AndroidJavaObject("android.util.DisplayMetrics");
            AndroidJavaObject activityInstance = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject windowManagerInstance = activityInstance.Call<AndroidJavaObject>("getWindowManager");
            AndroidJavaObject displayInstance = windowManagerInstance.Call<AndroidJavaObject>("getDefaultDisplay");

            displayInstance.Call("getMetrics", metricsInstance);
            Density = metricsInstance.Get<float>("density");

            arrayIndex = getOptimalSlotSize();
        }
    }

    public int getOptimalSlotSize()
    {
        double density = Density;

        double width = Screen.width;
        double height = Screen.height;

        for (int i = 0; i < maparray.Length; i++)
        {
            if (maparray[i][1] * density <= width
            && maparray[i][2] * density <= height)
            {
                return maparray[i][0];
            }
        }
        return 0;
    }

    private void Start()
    {
       /* MobileAds.Initialize(appId);
        Advertisement.Initialize(UnityAdId);
        interstitialDisplayTime = Time.time;

        request = new AdRequest.Builder()
             .AddExtra("is_designed_for_families", "true")
             .AddExtra("max_ad_content_rating", "G")
             .TagForChildDirectedTreatment(true)
             .Build();

        //currentAdsize = adSize_1;
        currentAdsize = AdSize.SmartBanner;

        LoadAdmobBanner();
        StartCoroutine(InitializeRoutine());*/
    }


    IEnumerator InitializeRoutine()
    {
        yield return new WaitForSeconds(5f);
        RequestAdmobInterstitial();
    }

    #region ADMOB BANNER   
    public void LoadAdmobBanner()
    {
        try
        {
           /* bannerView = new BannerView(adUnitId_AdmobBanner, currentAdsize, AdPosition.Bottom);
           // bannerView.OnAdFailedToLoad += AdmobBannerAdFailedToLoad;
            bannerView.LoadAd(request);*/
        }
        catch (Exception) { }

    }

/*    private void AdmobBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //if (currentAdsize == adSize_1)
        //{
        //    if (arrayIndex == 0)
        //    {
        //        currentAdsize = adSize_2;
        //    }
        //    else
        //    {
        //        currentAdsize = adSize_3;
        //    }
        //    StartCoroutine(LoadAnotherAdmobBanner());
        //}
        //else if (currentAdsize == adSize_2)
        //{
        //    currentAdsize = adSize_3;
        //    StartCoroutine(LoadAnotherAdmobBanner());
        //}
        //else
        //{
        //    currentAdsize = adSize_1;
        //    instance.StartCoroutine(LoadAnotherAdmobBanner());
        //}
        currentAdsize = AdSize.SmartBanner;
        instance.StartCoroutine(LoadAnotherAdmobBanner());
    }*/

    IEnumerator LoadAnotherAdmobBanner()
    {
        yield return new WaitForSeconds(0.5f);
        try
        {
           /* if (bannerView != null)
                bannerView.Destroy();*/
        }
        catch (Exception) { }

        yield return new WaitForSeconds(0.5f);
        LoadAdmobBanner();
    }

    public void HideAdmobBanner()
    {
        try
        {
           /* if (bannerView != null)
                bannerView.Hide();*/
        }
        catch (Exception)
        { }
    }

    public void ShowAdmobBanner()
    {
        try
        {
           /* if (bannerView != null)
                bannerView.Show();*/
        }
        catch (Exception)
        { }
    }

    #endregion



    #region ADMOB INTERSTITIAL

    public void RequestAdmobInterstitial()
    {
        try
        {
            /*if (interstitial != null)
            {
               *//* if (!interstitial.IsLoaded())
                {
                    interstitial.LoadAd(request);
                }*//*
            }
            else
            {
                *//*interstitial = new InterstitialAd(adUnitId_AdmobInterstitial);
                interstitial.OnAdClosed += OnAdmobInterstitialClosed;
                interstitial.OnAdOpening += OnAdmobInterstitialOpened;
                interstitial.OnAdFailedToLoad += OnAdmobInterstitialFailedToLoad;
                interstitial.LoadAd(request);*//*
            }*/
        }
        catch (Exception) { }

    }
/*
    private void OnAdmobInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        try
        {
            *//*if (args.Message.ToLower().Contains("no fill"))
            {
                StartCoroutine(RequestAnotherAdmobInterstitial(10f));
            }
            else
            {
                StartCoroutine(RequestAnotherAdmobInterstitial(30f));
            }*//*
        }
        catch (Exception)
        { }
    }*/

    private void OnAdmobInterstitialClosed(object sender, EventArgs e)
    {
        // Time.timeScale = 1;
        StartCoroutine(RequestAnotherAdmobInterstitial(5f));
    }


    IEnumerator RequestAnotherAdmobInterstitial(float delay)
    {
        try
        {
           /* if (interstitial != null)
                interstitial.Destroy();*/
        }
        catch (Exception) { }
        yield return new WaitForSeconds(delay);
        RequestAdmobInterstitial();
    }

    private void OnAdmobInterstitialOpened(object sender, EventArgs e)
    {
        //    Time.timeScale = 0;
    }

    #endregion

    #region UNITY 
    static void LoadUnityInterstitial()
    {
       /* if (Advertisement.IsReady())
        {
            Advertisement.Show("video", new ShowOptions() { resultCallback = HandleAdResults });
        }*/
    }

    static void HandleAdResults(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Finished");
                break;
            case ShowResult.Failed:
                Debug.Log("Failed");
                break;
            case ShowResult.Skipped:
                Debug.Log("Skipped");
                break;
        }
    }
    #endregion


    #region DISPLAY INTERSTITIAL

    public static void DisplayInterstitial(int incrementBy = 2)
    {
        try
        {
            //if (IAPController.removeAdsStatus == IAPController.IAPStatus.NOT_PURCHASED)
            //{
                gamePlayIndex = gamePlayIndex + incrementBy;

                if (Time.time - interstitialDisplayTime < interstitialDisplayInterval)
                    return;

                if (gamePlayIndex < interstitialDisplyIndex)
                    return;

               /* if (interstitial.IsLoaded())  // admob
                {
                    gamePlayIndex = 0;
                    interstitialDisplayTime = Time.time;
                    interstitial.Show();
                }*/
               /* else if (Advertisement.IsReady())  // unity ads
                {
                    gamePlayIndex = 0;
                    interstitialDisplayTime = Time.time;
                    LoadUnityInterstitial();
                }*/
           // }
        }
        catch (Exception) { }


    }
    #endregion

    private void OnDisable()
    {
        try
        {
           /* if (bannerView != null)
               // bannerView.OnAdFailedToLoad -= AdmobBannerAdFailedToLoad;
            if (interstitial != null)
            {
                *//*interstitial.OnAdFailedToLoad -= OnAdmobInterstitialFailedToLoad;
                interstitial.OnAdClosed -= OnAdmobInterstitialClosed;*//*
            }*/
        }
        catch (Exception) { }
    }

    private void OnDestroy()
    {
        try
        {
            StopAllCoroutines();
        }
        catch (Exception) { }
    }
}
