using IoCPlus;

public class WeaponContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<HookContext>();

        On<DraggingInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<SlowTimeCommand>()
            .Do<CharacterPointToDirectionCommand>()
            .Do<CharacterUpdateAimLineDestinationCommand>()
            .Do<DispatchAimWeaponEventCommand>();

        On<ReleaseInDirectionInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<StopSlowTimeCommand>()
            .Do<CharacterStopAimLineCommand>()
            .Do<DispatchFireWeaponEventCommand>();

        On<CancelDragInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<CharacterStopAimLineCommand>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>()
            .Dispatch<CancelAimWeaponEvent>();
    }
}