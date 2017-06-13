using IoCPlus;

public class LevelUIPlayContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/Level/Play/PauseButtonUI", CanvasLayer.UI)
            .Do<PauseTimeCommand>(false);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/Level/Play/PauseButtonUI", CanvasLayer.UI);
    }

}
