using IoCPlus;

public class FinishSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorInput> levelEditorInputRef;

    protected override void Execute() {
        levelEditorInputRef.Get().FinishSelectionField();
    }

}
