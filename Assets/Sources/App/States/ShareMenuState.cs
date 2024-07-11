using UnityEngine;

public class ShareMenuState : ScreenState<ShareMenuScreen>, IScreenState {

    public ShareMenuState(IPresenter[] presenters, ShareMenuScreen view) : base(presenters, view) {}

}

[PresenterOf(typeof(ShareMenuState))]
public class ShareMenuPresenter : IPresenter {

    private const string URL_MARKET = "https://play.google.com/store/apps/details?id=";
    private const string DIRECT_URL_MARKET = "market://details?id=";
    
    private readonly ShareMenuScreen _screen;
    private readonly IButtonCommand _shareCommand;
    private readonly IButtonCommand _continueCommand;

    public ShareMenuPresenter(IAppStates states, ShareMenuScreen screen) {
        _screen = screen;
        
        _continueCommand = ButtonCommand.Create(states.ChangeState<MainMenuState>);
        _shareCommand = ButtonCommand.Create(() => {
            var share = new NativeShare();
            share.SetUrl($"{URL_MARKET}{Application.identifier}")
                .SetTitle("Отправь ссылку другу!")
                .SetCallback((r, t) => {
                    if(r == NativeShare.ShareResult.Shared) _continueCommand.Execute();
                })
                .Share();
        });
    }
    
    public void Enter() {
        _screen.OnButtonClick<InviteFriendsButton>(_shareCommand);
    }

    public void Exit() {}
}