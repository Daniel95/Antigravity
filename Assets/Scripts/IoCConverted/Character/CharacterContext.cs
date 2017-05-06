using IoCPlus;

public class CharacterContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<Ref<ICharacterAimLine>>();
        Bind<Ref<ICharacterBounce>>();
        Bind<Ref<ICharacterCollisionDirection>>();
        Bind<Ref<ICharacterCollisionHitDetection>>();
        Bind<Ref<ICharacterDie>>();
        Bind<Ref<ICharacterDirectionPointer>>();
        Bind<Ref<ICharacterJump>>();
        Bind<Ref<ICharacterRaycast>>();
        Bind<Ref<ICharacterSpeed>>();
        Bind<Ref<ICharacterTurnDirection>>();
        Bind<Ref<ICharacterVelocity>>();

        On<EnterContextSignal>()
            .Do<ActivateViewOnPlayerCommand<CharacterVelocityView>>()
            .Do<CharacterSetSavedDirectionToStartDirectionCommand>();

        On<JumpInputEvent>()
            .Do<CharacterTryJumpCommand>();

        On<CharacterRetryJumpEvent>()
            .Do<CharacterRetryJumpCommand>();

        On<CharacterJumpEvent>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>()
            .Do<CharacterJumpCommand>();

        On<CharacterBounceEvent>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>()
            .Do<CharacterBounceCommand>();

        On<CharacterRemoveCollisionDirectionEvent>()
            .Do<CharacterRemoveCollisionDirectionCommand>();

        On<CharacterResetCollisionDirectionEvent>()
            .Do<CharacterResetCollisionDirectionCommand>();

        On<CharacterTurnToNextDirectionEvent>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>()
            .Do<CharacterTurnToNextDirectionCommand>();

        On<CharacterUpdateLineDestinationEvent>()
            .Do<CharacterUpdateAimLineDestinationCommand>();

        On<CharacterStopAimLineEvent>()
            .Do<CharacterStopAimLineCommand>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfNotCollidingAndNotInTriggerKillerTagsCommand>();

        On<CharacterPointToDirectionEvent>()
            .Do<CharacterPointToDirectionCommand>();

        On<CharacterSetMoveDirectionEvent>()
            .Do<CharacterSetMoveDirectionCommand>();

        On<CharacterSetVelocityEvent>()
            .Do<CharacterSetVelocityCommand>();
    }
}