using IoCPlus;

public class AbortIfContextStateIsNotCommand : Command<Context> {

    [Inject] private IContext context;

    protected override void Execute(Context context) {
        if(context.State != context) {
            Abort();
        }
    }

}
