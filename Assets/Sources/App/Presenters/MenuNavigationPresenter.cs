public abstract class MenuNavigationPresenter<TScreen> : IPresenter where TScreen: MenuScreen {
    private readonly AdService _service;
    private readonly TScreen _screen;

    private readonly IButtonCommand _mainMenuCommand;
    private readonly IButtonCommand _shareMenuCommand;
    private readonly IButtonCommand _payingMenuCommand;
    private readonly IButtonCommand _moreMenuCommand;

    public MenuNavigationPresenter(IAppStates states, AdService service, TScreen screen) {
        _service = service;
        _screen = screen;

        _mainMenuCommand = ButtonCommand.Create(states.ChangeState<MainMenuState>);
        _shareMenuCommand = ButtonCommand.Create(states.ChangeState<ShareMenuState>);
        _payingMenuCommand = ButtonCommand.Create(states.ChangeState<PayingMenuState>);
        _moreMenuCommand = ButtonCommand.Create(states.ChangeState<MoreMenuState>);
    }
    
    public void Enter() {
        _mainMenuCommand.State = _screen is not MainMenuScreen;
        _shareMenuCommand.State = _screen is not ShareMenuScreen;
        _payingMenuCommand.State = _screen is not PayingMenuScreen;
        _moreMenuCommand.State = _screen is not MoreMenuScreen;
        
        _screen.OnButtonClick<MainMenuTabButton>(_mainMenuCommand);
        _screen.OnButtonClick<ShareMenuTabButton>(_shareMenuCommand);
        _screen.OnButtonClick<PayingMenuTabButton>(_payingMenuCommand);
        _screen.OnButtonClick<MoreMenuTabButton>(_moreMenuCommand);
        
        _service.AdLoad();
        _service.ShowBanner();
    }

    public void Exit() {
        _service.HideBanner();
    }
}

[PresenterOf(typeof(MainMenuState))]
public class MainMenuNavigationPresenter : MenuNavigationPresenter<MainMenuScreen> {
    public MainMenuNavigationPresenter(IAppStates states, AdService service, MainMenuScreen screen) : base(states, service, screen) { }
}

[PresenterOf(typeof(ShareMenuState))]
public class ShareMenuNavigationPresenter : MenuNavigationPresenter<ShareMenuScreen> {
    public ShareMenuNavigationPresenter(IAppStates states, AdService service, ShareMenuScreen screen) : base(states, service, screen) { }
}

[PresenterOf(typeof(PayingMenuState))]
public class PayingMenuNavigationPresenter : MenuNavigationPresenter<PayingMenuScreen> {
    public PayingMenuNavigationPresenter(IAppStates states, AdService service, PayingMenuScreen screen) : base(states, service, screen) { }
}

[PresenterOf(typeof(MoreMenuState))]
public class MoreMenuNavigationPresenter : MenuNavigationPresenter<MoreMenuScreen> {
    public MoreMenuNavigationPresenter(IAppStates states, AdService service, MoreMenuScreen screen) : base(states, service, screen) { }
}
