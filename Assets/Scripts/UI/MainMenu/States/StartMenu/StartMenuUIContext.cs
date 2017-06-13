using IoCPlus;

public class StartMenuUIContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/PlayButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/GoToControlsMenuButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/StartMenuTextUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/MainMenu/StartMenu/PlayButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/MainMenu/StartMenu/GoToControlsMenuButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/MainMenu/StartMenu/StartMenuTextUI", CanvasLayer.UI);
    }

}