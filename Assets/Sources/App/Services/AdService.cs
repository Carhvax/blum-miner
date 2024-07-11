using System;
using CAS;
using UnityEngine;

public enum AdCompleteType { Closed, Completed }

public class AdQueue : IDisposable {
    
    private readonly IMediationManager _manager;
    private Action<AdCompleteType> _completedRequestAd;

    public bool AdLoaded => /*_manager.IsReadyAd(AdType.Interstitial) ||*/ _manager.IsReadyAd(AdType.Rewarded);
    
    public AdQueue(IMediationManager manager) {
        _manager = manager;
        
        _manager.OnRewardedAdCompleted += OnRewardedAdCompleted;
        _manager.OnRewardedAdClosed += OnRewardedAdClosed;
        _manager.OnRewardedAdLoaded += OnRewardedAdLoaded;
        
        _manager.OnInterstitialAdLoaded += OnInterstitialAdLoaded;
        _manager.OnInterstitialAdClosed += OnInterstitialAdClosed;
        
        _manager.LoadAd(AdType.Rewarded);
        //_manager.LoadAd(AdType.Banner);
    }

    public void PlayAd(Action<AdCompleteType> complete) {
        _completedRequestAd = complete;
        _manager.ShowAd(AdType.Rewarded);
    }
    
    private void OnRewardedAdClosed() {
        _manager.LoadAd(AdType.Rewarded);
        _completedRequestAd?.Invoke(AdCompleteType.Closed);
    }
    
    private void OnRewardedAdCompleted() {
        _manager.LoadAd(AdType.Rewarded);
        _completedRequestAd?.Invoke(AdCompleteType.Completed);
    }
    
    private void OnInterstitialAdClosed() {
        _manager.LoadAd(AdType.Interstitial);
        _completedRequestAd?.Invoke(AdCompleteType.Completed);
    }
    
    private void OnRewardedAdLoaded() {
        
    }

    private void OnInterstitialAdLoaded() {
        
    }

    public IAdView CreateBanner() {
        
        var view = _manager.GetAdView(AdSize.AdaptiveFullWidth);
        view.position = AdPosition.BottomCenter;
        view.refreshInterval = 30;
        view.Load();
        view.SetActive(true);

        return view;
    }

    public void Dispose() {
        _manager.OnRewardedAdCompleted -= OnRewardedAdCompleted;
        _manager.OnRewardedAdClosed -= OnRewardedAdClosed;
        _manager.OnRewardedAdLoaded -= OnRewardedAdLoaded;
        
        _manager.OnInterstitialAdLoaded -= OnInterstitialAdLoaded;
        _manager.OnInterstitialAdClosed -= OnInterstitialAdClosed;
    }
    public void Load(AdType type = AdType.Rewarded) {
        _manager.LoadAd(type);
    }
}

public class AdService : IDisposable {
    
    private readonly AdQueue _manager;
    private IAdView _banner;

    public bool AdState => _manager.AdLoaded;
    
    public ObservableField<bool> RewardState { get; } = new ObservableBool(false);
    
    public AdService() {
        var mediationManager = MobileAds
            .BuildManager()
            .Build();
        
        mediationManager.OnRewardedAdLoaded += OnRewardedAdLoaded;
        mediationManager.OnRewardedAdImpression += OnRewardedAdImpression;
        mediationManager.OnInterstitialAdImpression += OnRewardedAdImpression;
        
        _manager = new AdQueue(mediationManager);
    }

    private void OnRewardedAdImpression(AdMetaData meta) => CollectImpression(meta);

    private void CollectImpression(AdMetaData meta) {}

    private void OnRewardedAdLoaded() {
        RewardState.Value = _manager.AdLoaded;
    }

    public void ShowAd(Action<AdCompleteType> complete) => _manager.PlayAd(complete);

    public void ShowBanner() {
        _banner = _manager.CreateBanner();
    }

    public void HideBanner() {
        if(_banner == null) return;
        
        _banner.SetActive(false);
        _banner.Dispose();
    }
    
    public void Dispose() => _manager.Dispose();
    
    public void AdLoad() {
        _manager.Load();
    }
}