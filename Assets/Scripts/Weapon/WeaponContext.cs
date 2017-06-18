using IoCPlus;

public class WeaponContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<HookContext>();

        On<DraggingInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<AbortIfWeaponIsNotEnabledCommand>()
            .Do<SlowTimeCommand>()
            .Do<CharacterPointToDirectionCommand>()
            .Do<PlayerUpdateAimLineDestinationCommand>();

        On<ReleaseInDirectionInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<AbortIfWeaponIsNotEnabledCommand>()
            .Do<WeaponSetShootDirectionCommand>()
            .Do<CharacterPointToShootDirectionCommand>()
            .Do<DispatchFireWeaponEventCommand>();

        On<CancelDragInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<AbortIfWeaponIsNotEnabledCommand>()
            .Do<PlayerStopAimLineCommand>()
            .Dispatch<CancelAimWeaponEvent>();

        On<ReleaseInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<AbortIfWeaponIsNotEnabledCommand>()
            .Do<StopSlowTimeCommand>()
            .Do<PlayerStopAimLineCommand>();

        On<HoldingInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<AbortIfWeaponIsNotEnabledCommand>()
            .Do<SlowTimeCommand>();
    }
}