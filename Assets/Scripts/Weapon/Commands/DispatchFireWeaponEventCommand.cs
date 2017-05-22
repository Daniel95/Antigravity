using IoCPlus;
using UnityEngine;

public class DispatchFireWeaponEventCommand : Command {

    [Inject] private Ref<IAimDestination> aimDestination;
    [Inject] private FireWeaponEvent fireWeaponEvent;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        fireWeaponEvent.Dispatch(new FireWeaponEvent.Parameter(
            aimDestination.Get().GetDestinationPoint(direction), 
            aimDestination.Get().SpawnPosition)
        );
    }
}
