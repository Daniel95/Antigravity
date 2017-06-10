using IoCPlus;

public class LevelUIPauseContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/Level/Pause/PlayButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/Level/Pause/GoToMainMenuButtonUI", CanvasLayer.UI)
            .Do<PauseTimeCommand>(true);

    }

}