using IoCPlus;

public class SetSavedDirectionToCeilVelocityDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterTurnDirection> playerTurnDirectionViewRef;
    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerTurnDirectionViewRef.Get().SavedDirection = playerVelocityRef.Get().GetCeilVelocityDirection();
    }
}
