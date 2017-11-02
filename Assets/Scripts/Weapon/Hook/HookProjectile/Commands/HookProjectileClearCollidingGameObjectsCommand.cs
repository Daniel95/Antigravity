using IoCPlus;

public class HookProjectileClearCollidingGameObjectsCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookProjectileRef.Get().CollidingGameObjects.Clear();
    }

}
