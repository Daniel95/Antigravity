using IoCPlus;

public class PlayerDirectionPointerView : CharacterDirectionPointerView {

    [Inject(Label.Player)] private Ref<ICharacterDirectionPointer> playerDirectionPointerRef;

    public override void Initialize() {
        playerDirectionPointerRef.Set(this);
    }
}
