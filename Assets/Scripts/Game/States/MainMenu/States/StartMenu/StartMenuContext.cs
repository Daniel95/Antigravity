using IoCPlus;

public class StartMenuContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/PlayButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/GoToControlsMenuButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/StartMenuTextUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<RemoveViewFromCanvasLayerCommand>("PlayButtonUI", CanvasLayer.UI)
            .Do<RemoveViewFromCanvasLayerCommand>("GoToControlsMenuButtonUI", CanvasLayer.UI)
            .Do<RemoveViewFromCanvasLayerCommand>("StartMenuTextUI", CanvasLayer.UI);
    }

}