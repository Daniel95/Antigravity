using IoCPlus;

public class UpdateSelectionFieldToSwipePositionCommand : Command {

    [Inject] private Ref<ILevelEditorBuildingInput> levelEditorInputRef;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        levelEditorInputRef.Get().UpdateSelectionField(swipeMoveEventParameter.Position);
    }

}
