using System;
using UnityEngine;
using Zenject;

public class AppEntry : MonoBehaviour, IInitializable, IAppStates {
    
    private AppClient _client;
    private Action _focusChanged;

    [Inject]
    private void Construct(AppClient client) {
        _client = client;
    }
    
    public void Initialize() {
        _client.Start();        
    }

    private void OnApplicationFocus(bool hasFocus) {
        if(hasFocus)
            _focusChanged?.Invoke();
    }

    private void OnApplicationQuit() {
        _client.Stop();
    }

    public void ChangeState<TState>() where TState : class, IScreenState => _client.Change<TState>();
    public void ShowPopup<TState>() where TState : class, IScreenState => _client.Overlap<TState>();

    public void ReturnBack() => _client.ReturnBack();

}
