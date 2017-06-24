using IoCPlus;

public class LevelSelectUIContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GoToMainMenuUIStateEvent>();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelSelect/GoToMainMenuButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelSelect/GoToMainMenuButtonUI", CanvasLayer.UI);
    }

}