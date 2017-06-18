using IoCPlus;

public class PlayerSpeedView : CharacterSpeedView {

    [Inject(Label.Player)] private Ref<ICharacterSpeed> playerSpeedRef;

    public override void Initialize() {
        playerSpeedRef.Set(this);
    }
}
