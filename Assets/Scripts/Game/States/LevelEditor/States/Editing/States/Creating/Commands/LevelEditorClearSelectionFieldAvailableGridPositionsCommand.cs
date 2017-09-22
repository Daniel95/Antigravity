using IoCPlus;

public class LevelEditorClearSelectionFieldAvailableGridPositionsCommand : Command {

    [Inject] private Ref<ILevelEditorCreating> levelEditorCreatingRef;

    protected override void Execute() {
        levelEditorCreatingRef.Get().ClearSelectionFieldAvailableGridPositions();
    }

}
