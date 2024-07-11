using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class TabButton : ButtonElement {

    [SerializeField] private Image _icon;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _field;

    [Space] 
    [SerializeField] private Color _activeColor = new (1f, 0.41f, 0.26f, 1f);
    [SerializeField] private Color _passiveColor = new (0.46f, 0.47f, 0.5f);

    private void OnValidate() {
        _icon = GetComponentsInChildren<Image>().FirstOrDefault(i => i.name == "Icon");
        _background = GetComponent<Image>();
        _field = GetComponentInChildren<TMP_Text>();

        name = $"[{GetType().Name}]";
    }
    
    private void OnEnable() {
        var state = IsInteractable;
        
        var color = state ? _passiveColor: _activeColor;

        //_background.DOFade(state? 0: 1, 0);
        
        _icon.color = color;
        _field.color = color;
    }
}