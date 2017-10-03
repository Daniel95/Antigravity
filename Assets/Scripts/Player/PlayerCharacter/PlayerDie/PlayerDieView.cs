using IoCPlus;

public class PlayerDieView : CharacterDieView {

    [Inject(Label.Player)] private Ref<ICharacterDie> playerDieRef;

    public override void Initialize() {
        playerDieRef.Set(this);
    }
}
