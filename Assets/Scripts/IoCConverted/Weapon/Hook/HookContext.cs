using IoCPlus;

public class HookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GrapplingHookStartedEvent>();
        Bind<CancelGrapplingHookEvent>();

        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<IPullingHook>>();
        Bind<Ref<IHookView>>();


        On<EnterContextSignal>();

        On<CancelGrapplingHookEvent>()
            .Do<StopSlowTimeCommand>();

        On<FireWeaponEvent>();


        On<AimWeaponEvent>();

        On<CancelAimWeaponEvent>();

    }
}