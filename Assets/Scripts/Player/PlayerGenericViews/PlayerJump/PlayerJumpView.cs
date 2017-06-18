using IoCPlus;

public class PlayerJumpView : CharacterJumpView {

    [Inject(Label.Player)] private Ref<ICharacterJump> playerJumpRef;

    public override void Initialize() {
        playerJumpRef.Set(this);
    }
}
