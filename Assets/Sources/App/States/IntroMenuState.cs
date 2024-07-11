using UnityEngine;

public class IntroMenuState : ScreenState<IntroMenuScreen>, IScreenState {

    public IntroMenuState(IPresenter[] presenters, IntroMenuScreen view) : base(presenters, view) {}

}

[PresenterOf(typeof(IntroMenuState))]
public class IntroMenuPresenter : IPresenter {
    private readonly IntroMenuScreen _screen;
    private readonly IButtonCommand _continueCommand;

    public IntroMenuPresenter(IAppStates states, IntroMenuScreen screen) {
        _screen = screen;
        
        _continueCommand = ButtonCommand.Create(states.ChangeState<MainMenuState>);
    }
    
    public void Enter() {
        PlayerPrefs.SetInt("FirstTime", 1);
        
        _screen.OnButtonClick<BeginPlayButton>(_continueCommand);
    }

    public void Exit() {
        
    }
}