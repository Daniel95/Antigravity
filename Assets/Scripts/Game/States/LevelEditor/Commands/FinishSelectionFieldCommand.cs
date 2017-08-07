using IoCPlus;

public class FinishSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorBuildingInput> levelEditorInputRef;

    protected override void Execute() {
        levelEditorInputRef.Get().FinishSelectionField();
    }

}
