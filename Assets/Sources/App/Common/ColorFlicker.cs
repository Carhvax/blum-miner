using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ColorFlicker : MonoBehaviour {
    [SerializeField] private Image _image;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    private void OnValidate() {
        _image = GetComponent<Image>();
    }

    private void LateUpdate() {
        var progress = (1 + Mathf.Sin(Time.time)) / 2f;

        _image.color = Color.Lerp(_startColor, _endColor, progress);
    }
}
