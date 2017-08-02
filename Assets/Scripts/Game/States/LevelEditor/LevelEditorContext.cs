using IoCPlus;

public class LevelEditorContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<EnableCameraMoveInputCommand>(true)
            .Do<EnableCameraZoomInputCommand>(true);

        On<EnterContextSignal>()
            .Do<EnableCameraMoveInputCommand>(false)
            .Do<EnableCameraZoomInputCommand>(false);

        On<OutsideUITouchStartEvent>()
            .Do<StartSelectionFieldAtPositionCommand>();

        On<SwipeMovedEvent>()
            .Do<UpdateSelectionFieldToSwipePositionCommand>();

        On<TouchUpEvent>()
            .Do<FinishSelectionFieldCommand>();

        On<SwipeEndEvent>()
            .Do<FinishSelectionFieldCommand>();

    }

}