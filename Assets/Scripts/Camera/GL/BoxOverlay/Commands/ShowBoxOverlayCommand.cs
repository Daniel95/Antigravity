using IoCPlus;

public class ShowBoxOverlayCommand : Command<bool> {

    protected override void Execute(bool showBoxOverlay) {
        BoxOverlay.Instance.ShowBoxOverlay = showBoxOverlay;
    }

}
