using IoCPlus;

public class SpawnAnchorAtPlayerCommand : Command {

    [Inject] private PlayerModel playerModel;

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookRef.Get().AddAnchor(playerModel.Player.transform.position, hookProjectileRef.Get().Transform);
    }
}
