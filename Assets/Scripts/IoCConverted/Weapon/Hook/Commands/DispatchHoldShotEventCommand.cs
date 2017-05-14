using IoCPlus;

public class DispatchHoldShotEventCommand : Command {

    [Inject] private HoldShotEvent holdShotEvent;

    [InjectParameter] private FireWeaponEvent.FireWeapFireWeaponEventParameter fireWeapFireWeaponEventParameter;

    protected override void Execute() {
        holdShotEvent.Dispatch(new HoldShotEvent.HoldShotEventParameter(
            fireWeapFireWeaponEventParameter.Destination,
            fireWeapFireWeaponEventParameter.StartPosition
        ));
    }
}
