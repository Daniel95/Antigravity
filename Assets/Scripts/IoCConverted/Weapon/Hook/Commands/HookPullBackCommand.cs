using IoCPlus;

public class HookPullBackCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {

    }
}
