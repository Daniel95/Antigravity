using IoCPlus;

public class LevelEditorNavigatingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Navigating/GoToCreatingStateButtonUI", CanvasLayer.UI)
            .Do<EnableCameraZoomInputCommand>(true)
            .Do<EnableCameraMoveInputCommand>(true)
            .Do<SetCameraMoveInputTypeCommand>(CameraMoveInputType.Swipe);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Navigating/GoToCreatingStateButtonUI", CanvasLayer.UI)
            .Do<EnableCameraZoomInputCommand>(false)
            .Do<EnableCameraMoveInputCommand>(false);

    }

}