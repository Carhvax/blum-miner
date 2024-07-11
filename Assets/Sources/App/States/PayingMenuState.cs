public class PayingMenuState : ScreenState<PayingMenuScreen>, IScreenState {

    public PayingMenuState(IPresenter[] presenters, PayingMenuScreen view) : base(presenters, view) {}

}

[PresenterOf(typeof(PayingMenuState))]
public class PayingMenuPresenter : IPresenter {
    
    private readonly PayingMenuScreen _screen;
    private readonly IButtonCommand _linkCommand;

    public PayingMenuPresenter(IAppStates states, PayingMenuScreen screen) {
        _screen = screen;

        _linkCommand = ButtonCommand.Create(screen.Notify);
    }
    
    public void Enter() {
        _screen.OnButtonsClick<LinkToKeeperButton>(_linkCommand);
    }

    public void Exit() {}
}