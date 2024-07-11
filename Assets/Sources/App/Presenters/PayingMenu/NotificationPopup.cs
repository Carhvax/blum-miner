using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPopup : MonoBehaviour {
    [SerializeField] private RectTransform _popup;
    [SerializeField] private Button _closeButton;
    
    private Vector3 _hidePosition;

    private void Awake() {
        gameObject.SetActive(false);
    }

    public void Show() {
        _hidePosition = _popup.position;
        gameObject.SetActive(true);

        DoShowPopup(() => _closeButton.onClick.AddListener(Hide));
    }

    private void DoShowPopup(Action complete) {
        DOTween
            .Sequence()
            .Append(_popup.DOMove(_hidePosition + Vector3.up * (Screen.height * .5f), .25f))
            .OnComplete(() => complete?.Invoke())
            .Play();
    }

    private void Hide() {
        DOTween
            .Sequence()
            .Append(_popup.DOMove(_hidePosition, .25f))
            .OnComplete(() => {
                gameObject.SetActive(false);
                _closeButton.onClick.RemoveAllListeners();
            })
            .Play();
    }
}
