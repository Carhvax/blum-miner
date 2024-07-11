using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LocalizationItem {
    public string id;
    public string translation;
}

public class LocalizationInfo {
    public string id;
    public List<LocalizationItem> items;

    public bool TryGet(string param, out string result) {
        result = null;
        var item  = items.FirstOrDefault(i => i.id == param);

        if (item != null)
            result = item.translation;
        
        return !result.IsNullOrEmpty();
    }
}

public static class AppLocalization {
    
    private static LocalizationInfo[] _items;
    private static LocalizationInfo _defaultLang;
    private static string _lang;

    public static string SystemLang => _lang;
    
    public static void Ctor() {
        var source = Resources.Load<TextAsset>("localization");
        _lang = GetLang();
        if (source != null && !source.text.IsNullOrEmpty()) {
            SelectLocalization(_lang, source.Deserialize<LocalizationInfo[]>());
        }
    }

    private static string GetLang() {
        var region = new[] {
            SystemLanguage.Russian,
            SystemLanguage.Ukrainian,
            SystemLanguage.Belarusian,
        };
        
        var lang = region.Any(r => r == Application.systemLanguage)? "ru" : "en";

        return lang;
    }
    
    private static void SaveTranslation(string[] source) {
        var content = "[";
        
        source.Each(s => content += s + ",");
        content += "]";
        
        var path = Path.Combine(Application.dataPath, "Resources", "localization.json");
        
        File.WriteAllText(path, content);
    }

    private static void SelectLocalization(string lang, LocalizationInfo[] import) {
        if (import.Length > 0) {
            _items = import;
            _defaultLang = _items.FirstOrDefault(i => i.id == lang);
        }
    }

    public static string Translate(this string id) => Translate(id, null);
    
    public static string Translate(this string id, params object[] values) {
        if (_defaultLang.TryGet(id, out var translation)) {

            var result = translation;
            
            if(values != null)
                values.For((i, v) => {
                    result = result.Replace($"@{i + 1}", v.ToString());
                });
            
            return result.Replace("\\n","\n");
        }

        return id;
    }
}
