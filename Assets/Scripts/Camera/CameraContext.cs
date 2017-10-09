using IoCPlus;

public class CameraContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AddCameraContainerViewCommand>();

        On<TouchStartEvent>()
            .Do<AbortIfCameraMoveInputIsFalseCommand>()
            .Do<UpdateCameraVelocityPreviousTouchScreenPositionCommand>();

        On<SwipeMovedEvent>()
            .Do<AbortIfCameraMoveInputIsFalseCommand>()
            .Do<AbortIfCameraMoveInputTypeIsNotCommand>(CameraMoveInputType.Swipe)
            .Do<SwipeCameraCommand>();

        On<SwipeMoved2FingersEvent>()
            .Do<AbortIfCameraMoveInputIsFalseCommand>()
            .Do<AbortIfCameraMoveInputTypeIsNotCommand>(CameraMoveInputType.Swipe2Fingers)
            .Do<Swipe2FingersCameraCommand>();

        On<PinchMovedEvent>()
            .Do<AbortIfCameraZoomInputIsFalseCommand>()
            .Do<ZoomCameraCommand>();

    }

}