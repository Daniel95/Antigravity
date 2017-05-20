using IoCPlus;

public class SetHookDestinationCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    [InjectParameter] private ShootHookEvent.ShootHookEventParameter shootHookEventParameter;

    protected override void Execute() {
        hookRef.Get().Destination = shootHookEventParameter.Destination;
    }
}
