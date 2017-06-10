using IoCPlus;

public class StartMenuUIContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/PlayButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/GoToControlsMenuButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/StartMenuTextUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyViewFromCanvasLayerCommand>("PlayButtonUI", CanvasLayer.UI)
            .Do<DestroyViewFromCanvasLayerCommand>("GoToControlsMenuButtonUI", CanvasLayer.UI)
            .Do<DestroyViewFromCanvasLayerCommand>("StartMenuTextUI", CanvasLayer.UI);
    }

}