using IoCPlus;

public class HookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GrapplingHookStartedEvent>();
        Bind<CancelGrapplingHookEvent>();

        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<IPullingHook>>();

        On<EnterContextSignal>();

        On<CancelGrapplingHookEvent>();
            //.Do
    }
}