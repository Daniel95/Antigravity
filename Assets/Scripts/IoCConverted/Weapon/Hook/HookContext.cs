using IoCPlus;

public class HookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GrapplingHookStartedEvent>();
        Bind<GrapplingHookCancelledEvent>();

        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<IPullingHook>>();

        On<EnterContextSignal>();

        On<GrapplingHookCancelledEvent>();
            //.Do
    }
}