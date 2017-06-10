using IoCPlus;

public class ControlsMenuContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/GoToStartMenuButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfPlatformIsMobileCommand>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/ControlsMenu/ControlsTextPCUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfPlatformIsNotMobileCommand>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/MainMenu/ControlsMenu/ControlsTextMobileUI", CanvasLayer.UI);


        On<LeaveContextSignal>()
            .Do<RemoveViewFromCanvasLayerCommand>("GoToStartMenuButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfPlatformIsMobileCommand>()
            .Do<RemoveViewFromCanvasLayerCommand>("ControlsTextPCUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfPlatformIsNotMobileCommand>()
            .Do<RemoveViewFromCanvasLayerCommand>("ControlsTextMobileUI", CanvasLayer.UI);

    }
}