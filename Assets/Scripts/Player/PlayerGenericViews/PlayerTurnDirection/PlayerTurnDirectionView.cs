using IoCPlus;

public class PlayerTurnDirectionView : CharacterTurnDirectionView {

    [Inject(Label.Player)] private Ref<ICharacterTurnDirection> playerTurnDirectionRef;

    public override void Initialize() {
        playerTurnDirectionRef.Set(this);
    }
}
