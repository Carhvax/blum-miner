using System;
using UnityEngine;

public class AccountModel : IUIKitModel, IDisposable {

    public static int SCORE_AMOUNT = 4000;
    
    public ObservableInt Scores { get; } = new ObservableInt(0);
    public ObservableFloat Delay { get; } = new ObservableFloat(0);
    public ObservableString AccountId { get; } = new ObservableString("");
    
    public AccountModel() {
        if(ES3.KeyExists("Scores"))
            Scores.Value = ES3.Load<int>("Scores");

        AccountId.Value = ES3.KeyExists("AccountId")? ES3.Load<string>("AccountId"): GenerateId();
    }

    private string GenerateId() {
        var result = "";
        for (var i = 0; i < 10; i++) {
            result += UnityEngine.Random.Range(0, 10).ToString();
        }

        return $"#{result}";
    }

    public void SetDelay(int seconds = 10) {
        Delay.Value = Time.time + 10;
    }
    
    public void UpdateScore() {
        Scores.Value += SCORE_AMOUNT;
        SetDelay();
        
        UpdateSave();
    }
    
    public void SubtractSecond() {
        Delay.Value -= 1;
    }
    
    private void UpdateSave() {
        ES3.Save("Scores", Scores.Value);
        ES3.Save("AccountId", AccountId.Value);
    }
    
    public void Dispose() {
        
    }

    
}
