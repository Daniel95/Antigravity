using IoCPlus;

public class DispatchSetlevelSelectButtonInteractableEventCommand : Command<bool> {

    [Inject] private SetLevelSelectButtonInteractableEvent setLevelSelectButtonInteractableEvent;

    protected override void Execute(bool interactable) {
        setLevelSelectButtonInteractableEvent.Dispatch(interactable);
    }

}
