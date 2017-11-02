using IoCPlus;

public class HookProjectileSetParentToFirstCollidingGameObjectsCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        if(hookProjectileRef.Get().CollidingGameObjects.Count != 0) {
            hookProjectileRef.Get().SetParent(hookProjectileRef.Get().CollidingGameObjects[0].transform);
        }
    }

}
