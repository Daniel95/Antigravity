using IoCPlus;

public class LevelEditorSetLevelEditorStatusCommand : Command<bool> {

    [Inject] private LevelEditorStatus levelEditorStatus;

    protected override void Execute(bool active) {
        levelEditorStatus.Active = active;
    }

}
