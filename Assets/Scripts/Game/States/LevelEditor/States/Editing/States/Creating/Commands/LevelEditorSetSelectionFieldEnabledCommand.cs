using IoCPlus;

public class LevelEditorSetSelectionFieldEnabledCommand : Command<bool> {

    protected override void Execute(bool enabled) {
        LevelEditorSelectionFieldStatusView.Enabled = enabled;
    }

}
