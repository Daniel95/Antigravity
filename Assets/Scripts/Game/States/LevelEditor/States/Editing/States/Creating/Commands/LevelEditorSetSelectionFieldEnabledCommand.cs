using IoCPlus;

public class LevelEditorSetSelectionFieldEnabledCommand : Command<bool> {

    [Inject] private LevelEditorSelectionFieldStatus levelEditorSelectionFieldStatus;

    protected override void Execute(bool enabled) {
        levelEditorSelectionFieldStatus.Enabled = enabled;
    }

}
