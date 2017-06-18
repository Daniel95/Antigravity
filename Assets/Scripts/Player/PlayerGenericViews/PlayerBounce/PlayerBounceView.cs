using IoCPlus;

public class PlayerBounceView : CharacterBounceView {

    [Inject(Label.Player)] private Ref<ICharacterBounce> playerBounceRef;

    public override void Initialize() {
        playerBounceRef.Set(this);
    }
}