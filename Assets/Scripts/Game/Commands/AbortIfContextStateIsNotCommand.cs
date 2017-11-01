using IoCPlus;

public class AbortIfContextStateIsNotCommand<T> : Command where T : IContext {

    [Inject] private IContext context;

    protected override void Execute() {
        if (context.State == null || context.State.GetType() != typeof(T)) {
            Abort();
        }
    }

}
