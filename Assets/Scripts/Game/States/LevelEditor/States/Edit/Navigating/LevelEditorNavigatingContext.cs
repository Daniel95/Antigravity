using IoCPlus;

public class LevelEditorNavigatingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Edit/Navigating/GoToCreatingStateButtonUI", CanvasLayer.UI)
            .Do<EnableCameraMoveInputCommand>(true)
            .Do<EnableCameraZoomInputCommand>(true);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Edit/Navigating/GoToCreatingStateButtonUI", CanvasLayer.UI)
            .Do<EnableCameraMoveInputCommand>(false)
            .Do<EnableCameraZoomInputCommand>(false);

        On<CameraZoomedEvent>()
            .Do<AbortIfGridOverlayIsNotShownCommand>()
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>();

    }

}