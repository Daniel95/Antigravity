using IoCPlus;

public class PlayerTrailView : CharacterTrailView {

    [Inject(Label.Player)] private Ref<ICharacterTrail> playerTrailRef;

    public override void Initialize() {
        playerTrailRef.Set(this);
    }
}
