using TMPro;
using UnityEngine;

public class TextLocalization : MonoBehaviour {
    [SerializeField] private TMP_Text _field;
    [SerializeField] private string _source;
    private void OnValidate() {
        _field = GetComponent<TMP_Text>();
        
        if (_source.IsNullOrEmpty())
            _source = _field.text;
    }

    private void OnEnable() {
        if (_field != null && !_field.text.IsNullOrEmpty()) {
            _field.text =_source.Translate();
        }
    }
}
