using System;
using UnityEngine;

public class AppBootState : IBootState {
    private readonly AdService _service;
    private readonly AccountModel _model;
    private readonly AppBootScreen _screen;
    private readonly IButtonCommand _continueLoadingCommand;

    public AppBootState(IAppStates states, AdService service, AccountModel model, AppBootScreen screen) {
        _service = service;
        _model = model;
        _screen = screen;

        var alreadyPlayed = PlayerPrefs.HasKey("FirstTime");
        
        _continueLoadingCommand = ButtonCommand.Create(alreadyPlayed? states.ChangeState<MainMenuState>: states.ChangeState<IntroMenuState>);
    }
    
    public void Enter(Action complete) {
        _screen.Show(complete);

        AppLocalization.Ctor();
        
        _model.SetDelay(5);
        _service.AdLoad();
        
        Delay.Execute(1f, _continueLoadingCommand.Execute);
    }

    public void Sleep(bool state, Action complete) => complete?.Invoke();

    public void Exit(Action complete) => _screen.Hide(complete);

}