using IoCPlus;

public class DispatchFireWeaponEventCommand : Command {

    [Inject] private Ref<IShoot> shootRef;
    [Inject] private FireWeaponEvent fireWeaponEvent;

    [Inject] private FireWeaponParameter fireWeaponParameter;

    protected override void Execute() {
        fireWeaponEvent.Dispatch(fireWeaponParameter);
    }
}
