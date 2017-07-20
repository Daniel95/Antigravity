using IoCPlus;

public class SetSavedDirectionToCeilVelocityDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterTurnDirection> playerTurnDirectionRef;
    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerTurnDirectionRef.Get().SavedDirection = playerVelocityRef.Get().GetCeilVelocityDirection();
    }
}
