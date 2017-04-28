using IoCPlus;

public class AbortIfHookProjectileAnchorIndexIsHigherOrEqualThenAnchorCount : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        if (hookProjectileRef.Get().ReachedAnchorsIndex >= hookRef.Get().Anchors.Count - 1) {
            Abort();
        }
    }
}
