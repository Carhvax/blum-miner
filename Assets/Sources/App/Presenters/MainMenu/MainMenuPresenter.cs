using DG.Tweening;
using UnityEngine;

[PresenterOf(typeof(MainMenuState))]
public class MainMenuPresenter : IPresenter {
    
    private readonly AccountModel _model;
    private readonly AdService _service;
    private readonly MainMenuScreen _screen;
    private readonly IButtonCommand _playAdCommand;
    private Sequence _timer;

    public MainMenuPresenter(AccountModel model, AdService service, MainMenuScreen screen) {
        _model = model;
        _service = service;
        _screen = screen;

        _playAdCommand = ButtonCommand.Create(() => {
            service.ShowAd((state) => {
                if(state == AdCompleteType.Completed)
                    _model.UpdateScore();
                
                service.AdLoad();
            });
        });
    }
    
    public void Enter() {
        _model.Scores.Changed += _screen.OnScoreChanged;
        _model.Delay.Changed += OnTimeOutChanged;
        _model.AccountId.Changed += _screen.OnAccountIdChanged;
        
        _screen.OnButtonClick<PlayAdButton>(_playAdCommand);
        
        _service.AdLoad();
    }

    private void OnTimeOutChanged(float nextDelay) {
        if (Time.time > nextDelay) {
            _playAdCommand.State = true;
            _screen.OnTimerChanged(0);
            return;
        }

        var seconds = (int)(nextDelay - Time.time);
        _playAdCommand.State = seconds <= 0;
        
        _screen.OnTimerChanged(seconds);

        if(seconds > 0)
            _timer = Delay
                .Execute(1, () => {
                    OnTimeOutChanged(nextDelay);
                });
    }

    public void Exit() {
        _model.AccountId.Changed -= _screen.OnAccountIdChanged;
        _model.Scores.Changed -= _screen.OnScoreChanged;
        _model.Delay.Changed -= OnTimeOutChanged;
        
        _screen.Clear();
        _timer?.Kill();
    }

}
