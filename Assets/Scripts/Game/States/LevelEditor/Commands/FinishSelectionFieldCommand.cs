using IoCPlus;

public class FinishSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    protected override void Execute() {
        levelEditorCreatingInputRef.Get().FinishSelectionField();
    }

}
