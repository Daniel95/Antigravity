using IoCPlus;

public class DispatchLevelEditorSaveLevelEventCommand : Command {

    [Inject] private LevelEditorSaveLevelEvent levelEditorSaveLevelEvent;

    [Inject] private Ref<ILevelEditorSavingLevelNameInputField> levelEditorSavingLevelNameInputFieldRef;

    protected override void Execute() {
        string levelName = levelEditorSavingLevelNameInputFieldRef.Get().Text;
        levelEditorSaveLevelEvent.Dispatch(levelName);
    }

}
