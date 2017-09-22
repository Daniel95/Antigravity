using IoCPlus;

public class ShowGridOverlayCommand : Command<bool> {

    protected override void Execute(bool showGridOverlay) {
        GridOverlay.Instance.ShowGridOverlay = showGridOverlay;
    }

}
