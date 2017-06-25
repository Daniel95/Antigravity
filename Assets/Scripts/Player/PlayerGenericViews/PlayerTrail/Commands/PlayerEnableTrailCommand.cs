using IoCPlus;

public class PlayerEnableTrailCommand : Command<bool> {

    [Inject(Label.Player)] private Ref<ICharacterTrail> playerTrailRef;

    protected override void Execute(bool enable) {
        if (enable) {
            playerTrailRef.Get().EnableTrail();
        } else {
            playerTrailRef.Get().DisableTrail();
        }
    }
}
