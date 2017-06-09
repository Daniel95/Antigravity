using IoCPlus;

public class StartMenuContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/PlayButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/ShowControlsButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/StartMenu/StartMenuTextUI", CanvasLayer.UI);
    }

}