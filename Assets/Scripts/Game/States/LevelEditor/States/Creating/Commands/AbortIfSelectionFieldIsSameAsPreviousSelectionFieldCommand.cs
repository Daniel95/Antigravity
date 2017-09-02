using IoCPlus;
using ExtraListExtensions;

public class AbortIfSelectionFieldIsSameAsPreviousSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    protected override void Execute() {
        if(levelEditorCreatingInputRef.Get().SelectionFieldGridPositions.Matches(levelEditorCreatingInputRef.Get().PreviousSelectionFieldGridPositions)) {
            Abort();
        }
    }

}
