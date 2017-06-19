using IoCPlus;

public class PlayerRaycastDirectionView : CharacterRaycastDirectionView {

    [Inject(Label.Player)] private Ref<ICharacterRaycastDirection> playerRaycastDirectionRef;

    public override void Initialize() {
        playerRaycastDirectionRef.Set(this);
    }
}
