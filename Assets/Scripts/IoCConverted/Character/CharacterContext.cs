using IoCPlus;

public class CharacterContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<ActivateViewOnPlayerCommand<CharacterVelocityView>>();

        On<StartSlidingMovementSignal>();

        On<JumpInputEvent>()
            .Do<TryJumpCommand>();
    }
}