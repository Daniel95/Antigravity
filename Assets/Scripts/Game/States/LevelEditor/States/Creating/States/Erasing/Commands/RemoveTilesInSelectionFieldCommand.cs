using IoCPlus;

public class RemoveTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    protected override void Execute() {
        levelEditorCreatingInputRef.Get().RemoveTilesInSelectionField();
    }

}
