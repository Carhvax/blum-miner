using UnityEngine;

public class MoreMenuState : ScreenState<MoreMenuScreen>, IScreenState {

    public MoreMenuState(IPresenter[] presenters, MoreMenuScreen view) : base(presenters, view) {}

}

[PresenterOf(typeof(MoreMenuState))]
public class MoreMenuPresenter : IPresenter {
    private const string DISCORD_URL = "https://discord.com";
    private const string TWITTER_URL = "https://twitter.com";
    
    private readonly MoreMenuScreen _screen;
    private readonly IButtonCommand _youtubeCommand;
    private readonly IButtonCommand _twitterCommand;

    public MoreMenuPresenter(MoreMenuScreen screen) {
        _screen = screen;

        _youtubeCommand = ButtonCommand.Create(() => ShowLink(DISCORD_URL));
        _twitterCommand = ButtonCommand.Create(() => ShowLink(TWITTER_URL));
    }

    private void ShowLink(string url) => Application.OpenURL(url);
    
    public void Enter() {
        _screen.OnButtonClick<OpenDiscordButton>(_youtubeCommand);
        _screen.OnButtonClick<OpenTwitterButton>(_twitterCommand);
    }

    public void Exit() {}
}