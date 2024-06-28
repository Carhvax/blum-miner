[PresenterOf(typeof(MainMenuState))]
public class MainMenuPresenter : IPresenter {

    private readonly MainMenuScreen _screen;

    public MainMenuPresenter(IAppStates states, MainMenuScreen screen) {
        _screen = screen;

        // commands bind here
    }
    
    public void Enter() {
        // bind command to view here
    }

    public void Exit() {
        
    }

}
