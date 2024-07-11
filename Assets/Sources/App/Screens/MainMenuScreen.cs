using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class MainMenuScreen : MenuScreen {
    [SerializeField] private TMP_Text _scoreField;
    [SerializeField] private TMP_Text _timerField;
    [SerializeField] private TMP_Text _accountIdField;

    [Space] 
    [SerializeField] private GameObject _playAd;
    [SerializeField] private GameObject _adCounter;
    
    private int _score;
    private Sequence _sequence;
    private Vector3 _defaultPosition;

    [Inject]
    private void Construct() {
        _defaultPosition = _timerField.transform.position;
    }
    public void OnScoreChanged(int score) {
        if (_score != score) {
            _scoreField.DOCounter(_score, score, "<sprite index=0>",.25f);
            _score = score;    
        }
    }
    
    public void OnAccountIdChanged(string id) => _accountIdField.text = id;

    public void OnTimerChanged(int seconds) {
        _adCounter.SetActive(seconds > 0);
        _playAd.SetActive( seconds <= 0);
        
        _timerField.text = seconds.ToString();

        if (seconds > 0) {
            _sequence = DOTween
                .Sequence()
                .Append(_timerField.DOFade(1, 0))
                .Append(_timerField.transform.DOPunchScale(Vector3.one * .25f, .5f))
                .Append(_timerField.DOFade(.1f, .5f))
                .Join(_timerField.transform.DOMove(_defaultPosition - Vector3.up * 50, .5f))
                .OnComplete(() => {
                    _timerField.transform.localScale = Vector3.one;
                    _timerField.transform.position = _defaultPosition;
                })
                .Play();
        }
    }

    public void Clear() {
        _sequence?.Kill(true);
    }
    
}