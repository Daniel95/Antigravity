using IoCPlus;

public class ReplaceNewTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorCreating> levelEditorCreatingRef;

    protected override void Execute() {
        levelEditorCreatingRef.Get().ReplaceNewTilesInSelectionField();
    }

}
