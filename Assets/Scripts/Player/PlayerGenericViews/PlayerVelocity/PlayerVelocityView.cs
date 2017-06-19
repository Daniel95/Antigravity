using IoCPlus;

public class PlayerVelocityView : CharacterVelocityView {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    public override void Initialize() {
        playerVelocityRef.Set(this);
    }
}
