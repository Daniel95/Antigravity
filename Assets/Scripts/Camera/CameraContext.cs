using IoCPlus;

public class CameraContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AddCameraContainerViewCommand>();

        On<TouchStartEvent>()
            .Do<AbortIfCameraMoveInputIsFalseCommand>()
            .Do<UpdateCameraVelocityPreviousTouchScreenPositionCommand>();

        On<SwipeStartEvent>()
            .Do<AbortIfCameraMoveInputIsFalseCommand>()
            .Do<AbortIfCameraMoveInputTypeIsNotCommand>(CameraMoveInputType.Swipe)
            .Do<StartSwipeCameraCommand>();

        On<SwipeMovedEvent>()
            .Do<AbortIfCameraMoveInputIsFalseCommand>()
            .Do<AbortIfCameraMoveInputTypeIsNotCommand>(CameraMoveInputType.Swipe)
            .Do<SwipeCameraCommand>();

        On<SwipeStart2FingersEvent>()
            .Do<AbortIfCameraMoveInputIsFalseCommand>()
            .Do<AbortIfCameraMoveInputTypeIsNotCommand>(CameraMoveInputType.Swipe2Fingers)
            .Do<StartSwipeCameraCommand>();

        On<SwipeMoved2FingersEvent>()
            .Do<AbortIfCameraMoveInputIsFalseCommand>()
            .Do<AbortIfCameraMoveInputTypeIsNotCommand>(CameraMoveInputType.Swipe2Fingers)
            .Do<Swipe2FingersCameraCommand>();

        On<PinchMovedEvent>()
            .Do<AbortIfCameraZoomInputIsFalseCommand>()
            .Do<AbortIfSwipeDelta2FingersBiggerThenPinchDeltaCommand>()
            .Do<ZoomCameraCommand>();

    }

}