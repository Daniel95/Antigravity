using IoCPlus;

public class SpawnAnchorAtPlayerCommand : Command {

    [Inject] private PlayerModel playerModel;

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        hookRef.Get().SpawnAnchor(playerModel.player.transform.position, hookRef.Get().HookProjectile.transform);
    }
}
