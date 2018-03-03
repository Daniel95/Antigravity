using IoCPlus;

public class DispatchSaveLevelEventCommand : Command {

    [Inject] private SaveCreatedLevelEvent saveLevelEvent;

    [Inject] private Ref<ILevelNameInputField> savingLevelNameInputFieldRef;

    protected override void Execute() {
        string levelName = savingLevelNameInputFieldRef.Get().Text;
        saveLevelEvent.Dispatch(levelName);
    }

}
