using IoCPlus;

public class HookProjectileSetReachedAnchorsIndexCommand : Command<int> {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute(int reachedAnchorsIndex) {
        hookProjectileRef.Get().AnchorsIndex = reachedAnchorsIndex;
    }
}
