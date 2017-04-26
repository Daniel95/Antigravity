using IoCPlus;

public class DispatchShootHookEventCommand : Command {

    [Inject] private ShootHookEvent shootHookEvent;

    [InjectParameter] private FireWeaponParameter fireWeaponParameter;

    protected override void Execute() {
        shootHookEvent.Dispatch(fireWeaponParameter);
    }
}
