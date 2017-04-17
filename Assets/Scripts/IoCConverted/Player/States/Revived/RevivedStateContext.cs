using IoCPlus;

public class RevivedStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewOnPlayerCommand<RevivedStateView>>()
            .Do<CharacterResetVelocityCommand>()
            .Do<CharacterResetMoveDirectionCommand>()
            .Do<CancelDragWeaponInputCommand>()
            .Do<DispatchEnableShootingInputCommand>(false)
            .Do<CharacterResetCollisionDirectionCommand>();

        On<DraggingInputEvent>()
            .Do<RevivedStateAimingCommand>();

        On<ReleaseInDirectionInputEvent>()
            .Do<RevivedStateLaunchCommand>()
            .Dispatch<ActivateFloatingStateEvent>();

    }
}