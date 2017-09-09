using IoCPlus;

public class ClearSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    protected override void Execute() {
        levelEditorCreatingInputRef.Get().ClearSelectionField();
    }

}
