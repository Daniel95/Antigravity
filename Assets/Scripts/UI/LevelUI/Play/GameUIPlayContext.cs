using IoCPlus;

public class GameUIPlayContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/GameUI/PlayUI", CanvasLayer.UI)
            .Do<PauseTimeCommand>(false);

    }

}
