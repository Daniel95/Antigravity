using IoCPlus;
using UnityEngine;

public class DispatchAimWeaponEventCommand : Command {

    [Inject] private Ref<IAimDestination> shootRef;
    [Inject] private AimWeaponEvent aimWeaponEvent;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        Vector2 destination = shootRef.Get().GetDestinationPoint(direction);

        aimWeaponEvent.Dispatch(destination);
    }
}
