using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAP_Manager : MonoBehaviour
{
    public const string NoAds_ID = "noads_findcolors";
    public const string Coins_1000_ID = "coins_1000";
    public const string Coins_2500_ID = "coins_2500";
    public const string Coins_5000_ID = "coins_5000";
    public const string Coins_10000_ID = "coins_10000";

    public void OnPurchaseComplete(Product product)
    {
        switch(product.definition.id)
        {
            case NoAds_ID:
                Debug.Log("Bought No Ads Successfully!!!");
                InAppRewards.Instance.On_No_Ads_Purchase_Success();
                break;
            case Coins_1000_ID:
                Debug.Log("Bought 1000 coins Successfully!!!");
                InAppRewards.Instance.On_Coins_Bought((int)product.definition.payout.quantity);
                break;
            case Coins_2500_ID:
                Debug.Log("Bought 2500 Successfully!!!");
                InAppRewards.Instance.On_Coins_Bought((int)product.definition.payout.quantity);
                break;
            case Coins_5000_ID:
                Debug.Log("Bought 5000 Successfully!!!");
                InAppRewards.Instance.On_Coins_Bought((int)product.definition.payout.quantity);
                break;
            case Coins_10000_ID:
                Debug.Log("Bought 10000 Successfully!!!");
                InAppRewards.Instance.On_Coins_Bought((int)product.definition.payout.quantity);
                break;

        }
    }

    public void OnPurchaseFailed(Product product , PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + "Failed Because : " + failureReason);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailedEventArgs failureReason)
    {
        Debug.Log(product.definition.id + "Failed Because : " + failureReason);
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureDescription p)
    {
        if (p.reason == PurchaseFailureReason.PurchasingUnavailable)
        {
            // IAP may be disabled in device settings.
            Debug.Log(i.definition.id + "Failed Because : " + p.message);
        }
    }
}
