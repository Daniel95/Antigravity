using IoCPlus;

public class RemoveTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorCreating> levelEditorCreatingRef;

    protected override void Execute() {
        levelEditorCreatingRef.Get().RemoveTilesInSelectionField();
    }

}
