using IoCPlus;

public class SetHookedLayerCommand : Command<int> {

    private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute(int layer) {
        hookProjectileRef.Get().HookedLayer = layer;
    }
}
