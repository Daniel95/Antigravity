using IoCPlus;

public class ReplaceNewTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    protected override void Execute() {
        levelEditorCreatingInputRef.Get().ReplaceNewTilesInSelectionField();
    }

}
