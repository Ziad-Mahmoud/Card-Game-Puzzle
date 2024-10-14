using GoogleMobileAds.Api;
using UnityEngine;
public class AdsManager : MonoBehaviour
{
    private string _InterstitialUnitId = "ca-app-pub-4985704153931149/9141882130";
    private string _RewardedUnitId = "ca-app-pub-4985704153931149/4397592420";
    private string _RewardedInterstitialUnitId = "ca-app-pub-4985704153931149/1995726146";

    public static InterstitialAd _interstitialAd;
    public static RewardedAd _rewardedAd;
    public static RewardedInterstitialAd _rewardedInterstitialAd;

    [SerializeField] LevelManager levelManager;
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        LoadInterstitial();
    }
    public void LoadInterstitial()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        InterstitialAd.Load(_InterstitialUnitId, new AdRequest(),
          (InterstitialAd ad, LoadAdError error) =>
          {
              if (error != null || ad == null)
                  return;
              _interstitialAd = ad;
          });
    }
    public void ShowInterstitial()
    {
        if(_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            levelManager.AddHeart();
            _interstitialAd.Show();
        }
    }
}