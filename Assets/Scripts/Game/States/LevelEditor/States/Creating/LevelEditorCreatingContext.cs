using IoCPlus;

public class LevelEditorCreatingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Building/GoToNavigatingStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Building/GoToNavigatingStateButtonUI", CanvasLayer.UI);

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