using GoogleMobileAds.Api;
using System;
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
    [SerializeField] Timer timer;
    [SerializeField] GameObject losePanel;

    public int adCounter = 1;
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        LoadInterstitial();
        LoadRewarded();
        LoadRewardedInterstitial();
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
            _interstitialAd.Show();
    }
    public void LoadRewarded()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        RewardedAd.Load(_RewardedUnitId, new AdRequest(),
          (RewardedAd ad, LoadAdError error) =>
          {
              if (error != null || ad == null)
                  return;
              _rewardedAd = ad;
          });
    }
    public void ShowRewarded(bool heart = true)
    {
        if (!heart && LevelManager.level > 95)
            if(adCounter > 10)
                return;

        if (_rewardedAd != null && _rewardedAd.CanShowAd()) 
            _rewardedAd.Show((e) => Reward(heart));
        else if (_rewardedInterstitialAd != null && _rewardedInterstitialAd.CanShowAd())
            _rewardedInterstitialAd.Show((e) => Reward(heart));
        else if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
            Reward(heart);
        }
    }
    public void LoadRewardedInterstitial()
    {
        if (_rewardedInterstitialAd != null)
        {
            _rewardedInterstitialAd.Destroy();
            _rewardedInterstitialAd = null;
        }

        RewardedInterstitialAd.Load(_RewardedInterstitialUnitId, new AdRequest(),
          (RewardedInterstitialAd ad, LoadAdError error) =>
          {
              if (error != null || ad == null)
                  return;
              _rewardedInterstitialAd = ad;
          });
    }
    void Reward(bool heart = true)
    {
        if (heart)
            levelManager.AddHeart();
        else
        {
            adCounter++;
            timer.Activate();
            timer.timeAllowed = TimeSpan.FromSeconds(10);
            timer.startTime = DateTime.Now;
            losePanel.SetActive(false);
        }
    }
}