using IoCPlus;

public class GameUIPauseContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewToCanvasLayerCommand>("UI/GameUI/PauseUI", CanvasLayer.UI)
            .Do<PauseTimeCommand>(true);

    }

}