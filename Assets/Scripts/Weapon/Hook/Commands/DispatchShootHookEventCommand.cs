using IoCPlus;

public class DispatchShootHookEventCommand : Command {

    [Inject] private ShootHookEvent shootHookEvent;

    [Inject] private Ref<IWeapon> weaponRef;

    protected override void Execute() {
        shootHookEvent.Dispatch(new ShootHookEvent.Parameter(
            weaponRef.Get().ShootDirection,
            weaponRef.Get().SpawnPosition
        ));
    }
}
