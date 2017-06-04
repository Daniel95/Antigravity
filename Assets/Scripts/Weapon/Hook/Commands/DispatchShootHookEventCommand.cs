using IoCPlus;

public class DispatchShootHookEventCommand : Command {

    [Inject] private ShootHookEvent shootHookEvent;

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        shootHookEvent.Dispatch(new ShootHookEvent.Parameter(
            hookRef.Get().ShootDirection,
            weaponRef.Get().SpawnPosition
        ));
    }
}
