using IoCPlus;

public class PlayerCollisionDirectionView : CharacterCollisionDirectionView {

    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    public override void Initialize() {
        playerCollisionDirectionRef.Set(this);
    }
}
