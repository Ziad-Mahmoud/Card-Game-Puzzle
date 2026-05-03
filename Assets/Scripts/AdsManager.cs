using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;
public class AdsManager : MonoBehaviour
{
    private readonly string _InterstitialUnitId = "ca-app-pub-4985704153931149/7814360118";
    private readonly string _RewardedUnitId = "ca-app-pub-4985704153931149/3875115100";
    private readonly string _RewardedInterstitialUnitId = "ca-app-pub-4985704153931149/6934616615";

    [SerializeField] Button watchAd, watchRewardedTimer, addHearts;
    [SerializeField] GameObject timer;
    [SerializeField] Timer myTimer;
    Button[] btns;
    bool adShown = false;
    bool adTimerShown = false;
    int tryReload = 0;
    void Awake()
    {
        btns = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        addHearts.onClick.AddListener(() => { LoadRewarded(true, true); });
        watchAd.onClick.AddListener(() => { LoadRewarded(true, true); } );
        watchRewardedTimer.onClick.AddListener(() => { LoadRewarded(false, true); } );
    }

    private void Update()
    {
        if (adShown)
        {
            foreach (Button b in btns)
                b.interactable = true;
            adShown = false;
        }
        if (adTimerShown)
        {
            timer.SetActive(true);
            adTimerShown = false;
        }
    }
    void DisableButtons()
    {
        foreach ( Button b in btns ) 
            b.interactable = false;
    }
    public void LoadInterstitial(bool heart, bool rewarded)
    {
        if (tryReload < 10)
            tryReload++;
        else
            return;
        DisableButtons();
        if (myTimer.adCounter > 1 && LevelManager.level > 95 && !heart && rewarded)
        {
            Invoke(nameof(EnableBtns), 2);
            return;
        }
        InterstitialAd.Load(_InterstitialUnitId, new AdRequest(), (InterstitialAd ad, LoadAdError error) =>
        {
            if (error == null && ad != null && ad.CanShowAd())
            {
                tryReload = 0;
                if (rewarded)
                    ad.OnAdFullScreenContentClosed += () => Reward(heart, rewarded);
                ad.Show();
            }
            else
                LoadRewarded(heart, rewarded);
            adShown = true;
        });
    }
    void EnableBtns()
    {
        adShown = true;
    }
    public void LoadRewarded(bool heart, bool rewarded)
    {
        if (tryReload < 10)
            tryReload++;
        else
            return;
        DisableButtons();
        if (myTimer.adCounter > 1 && LevelManager.level > 95 && !heart && rewarded)
        {
            Invoke(nameof(EnableBtns), 2);
            return;
        }
        RewardedAd.Load(_RewardedUnitId, new AdRequest(),
          (RewardedAd ad, LoadAdError error) =>
          {
              if (error == null && ad != null && ad.CanShowAd())
              {
                  tryReload = 0;
                  ad.Show((e) => Reward(heart, rewarded));
              }
              else
                  LoadRewardedInterstitial(heart, rewarded);
              adShown = true;
          });
    }
    public void LoadRewardedInterstitial(bool heart, bool rewarded)
    {
        if (tryReload < 10)
            tryReload++;
        else
            return;

        DisableButtons();
        if (myTimer.adCounter > 1 && LevelManager.level > 95 && !heart && rewarded)
        {
            Invoke(nameof(EnableBtns), 2);
            return;
        }
        RewardedInterstitialAd.Load(_RewardedInterstitialUnitId, new AdRequest(),
          (RewardedInterstitialAd ad, LoadAdError error) =>
          {
              if (error == null && ad != null && ad.CanShowAd())
              {
                  tryReload = 0;
                  ad.Show((e) => Reward(heart, rewarded));
              }
              else
                  LoadInterstitial(heart, rewarded);
              adShown = true;
          });
    }
    void Reward(bool heart, bool rewarded)
    {
        if (!rewarded) return;
        if (heart)
            LevelManager.trials++;
        else
        {
            adTimerShown = true;
            Timer.watchedAdTimer = true;
        }
    }
}