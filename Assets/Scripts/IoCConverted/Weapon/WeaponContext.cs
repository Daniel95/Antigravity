using IoCPlus;

public class WeaponContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<HookModel>();

        Bind<Ref<IShoot>>();
        Bind<Ref<IWeaponOutput>>();
        Bind<Ref<IPullingHook>>();
        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<IGrapplingHook>>();

        Bind<GrapplingHookStartedEvent>();
        Bind<ChangeSpeedByAngleEvent>();
        Bind<AddAnchorEvent>();
        Bind<CancelHookEvent>();

        On<EnterContextSignal>()
            .Do<ActivateViewOnPlayerCommand<ShootView>>();

        On<ReleaseInDirectionInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<StopSlowTimeCommand>()
            .Do<DispatchFireWeaponEventCommand>();

        On<DraggingInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<SlowTimeCommand>()
            .Do<CharacterPointToDirectionCommand>()
            .Do<CharacterUpdateAimLineDestinationCommand>()
            .Do<DispatchAimWeaponEventCommand>();

        On<CancelDragInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<StopSlowTimeCommand>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>()
            .Do<CancelAimWeaponOutputCommand>()
            .Dispatch<CancelAimWeaponEvent>();
    }
}