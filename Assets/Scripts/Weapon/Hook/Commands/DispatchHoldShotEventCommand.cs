using IoCPlus;

public class DispatchHoldShotEventCommand : Command {

    [Inject] private HoldShotEvent holdShotEvent;

    [InjectParameter] private FireWeaponEvent.Parameter fireWeapFireWeaponEventParameter;

    protected override void Execute() {
        holdShotEvent.Dispatch(new HoldShotEvent.HoldShotEventParameter(
            fireWeapFireWeaponEventParameter.Direction,
            fireWeapFireWeaponEventParameter.StartPosition
        ));
    }
}
