using IoCPlus;

public class DispatchLevelEditorSetlevelSelectButtonInteractableEventCommand : Command<bool> {

    [Inject] private LevelEditorSetLevelSelectButtonInteractableEvent setLevelSelectButtonInteractableEvent;

    protected override void Execute(bool interactable) {
        setLevelSelectButtonInteractableEvent.Dispatch(interactable);
    }

}
