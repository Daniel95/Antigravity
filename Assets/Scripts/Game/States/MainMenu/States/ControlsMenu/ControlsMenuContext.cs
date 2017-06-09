using IoCPlus;

public class ControlsMenuContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/BackToStartMenuUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfPlatformIsMobileCommand>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/ControlsMenu/ControlsTextPCUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfPlatformIsNotMobileCommand>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/ControlsMenu/ControlsTextMobileUI", CanvasLayer.UI);
    }

}