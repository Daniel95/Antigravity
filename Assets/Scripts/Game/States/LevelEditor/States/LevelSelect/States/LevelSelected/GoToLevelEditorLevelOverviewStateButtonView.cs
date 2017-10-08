using IoCPlus;

public class GoToLevelEditorLevelOverviewStateButtonView : ButtonListenerView {

    [Inject] private GoToLevelEditorLevelOverviewStateButtonClickedEvent levelOverviewStateButtonClickedEvent;

    protected override void OnButtonClick() {
        levelOverviewStateButtonClickedEvent.Dispatch();
    }

}
