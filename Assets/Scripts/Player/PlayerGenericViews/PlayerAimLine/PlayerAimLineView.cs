using IoCPlus;

public class PlayerAimLineView : CharacterAimLineView {

    [Inject(Label.Player)] private Ref<ICharacterAimLine> playerAimLineRef;

    public override void Initialize() {
        playerAimLineRef.Set(this);
    }

}
