using IoCPlus;

public class DispatchHoldShotEventCommand : Command {

    [Inject] private HoldShotEvent holdShotEvent;

    [InjectParameter] private FireWeaponParameter fireWeaponParameter;

    protected override void Execute() {
        holdShotEvent.Dispatch(fireWeaponParameter);
    }
}
