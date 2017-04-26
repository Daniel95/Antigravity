using IoCPlus;

public class ToolContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<Ref<IMoveTowards>>();

        On<StartMoveTowardsEvent>()
            .Do<StartMoveTowardsCommand>();

        On<StopMoveTowardsEvent>()
            .Do<StopSlowTimeCommand>();
    }
}