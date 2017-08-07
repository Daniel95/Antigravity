using IoCPlus;

public class UpdateSelectionFieldToSwipePositionCommand : Command {

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        levelEditorCreatingInputRef.Get().UpdateSelectionField(swipeMoveEventParameter.Position);
    }

}
