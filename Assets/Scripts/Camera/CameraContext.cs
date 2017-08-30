﻿using IoCPlus;

public class CameraContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AddCameraContainerViewCommand>();

        On<TouchStartEvent>()
            .Do<AbortIfCameraMoveInputIsFalseCommand>()
            .Do<UpdateCameraVelocityPreviousTouchScreenPositionCommand>();

        On<SwipeMovedEvent>()
            //.Do<DebugLogMessageCommand>("swipemoved")
            .Do<AbortIfCameraMoveInputIsFalseCommand>()
            .Do<SwipeCameraCommand>();

        On<PinchMovedEvent>()
            .Do<AbortIfCameraZoomInputIsFalseCommand>()
            .Do<ZoomCameraCommand>();

    }

}