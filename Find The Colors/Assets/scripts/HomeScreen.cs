using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    public Button NoAds_Btn;

    private void Start()
    {
        Set_No_Ads_Button();
    }
    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=6487105028651572662");
    }

    public void Rating()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.BabyBooApps.FindColors");
    }

    public void Set_No_Ads_Button()
    {
        NoAds_Btn.gameObject.SetActive(!PlayerPrefsManager.Instance.GetNoAdsStatus());
    }
}
