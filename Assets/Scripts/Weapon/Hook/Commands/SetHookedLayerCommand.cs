using IoCPlus;

public class SetHookedLayerCommand : Command<int> {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute(int layer) {
        hookProjectileRef.Get().HookedLayerIndex = layer;
    }
}
