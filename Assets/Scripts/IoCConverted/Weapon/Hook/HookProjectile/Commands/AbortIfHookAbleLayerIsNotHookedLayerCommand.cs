using IoCPlus;

public class AbortIfHookAbleLayerIsNotHookedLayerCommand : Command<int> {

[Inject] private Ref<IHookProjectile> hookProjectileRef;

protected override void Execute(int hookableLayer) {
    if (hookProjectileRef.Get().HookedLayerIndex != hookableLayer) {
        Abort();
    }
}
}
