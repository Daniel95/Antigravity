using IoCPlus;

public class DispatchShootHookEventCommand : Command {

    [Inject] private ShootHookEvent shootHookEvent;

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IAimDestination> aimDestination;

    protected override void Execute() {
        shootHookEvent.Dispatch(new ShootHookEvent.Parameter(
            hookRef.Get().Destination,
            aimDestination.Get().SpawnPosition
        ));
    }
}
