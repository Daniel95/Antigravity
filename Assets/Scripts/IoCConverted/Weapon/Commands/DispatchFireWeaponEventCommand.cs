using IoCPlus;
using UnityEngine;

public class DispatchFireWeaponEventCommand : Command {

    [Inject] private Ref<IShoot> shootRef;
    [Inject] private FireWeaponEvent fireWeaponEvent;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        fireWeaponEvent.Dispatch(new FireWeaponParameter(shootRef.Get().GetDestinationPoint(direction), shootRef.Get().SpawnPosition));
    }
}
