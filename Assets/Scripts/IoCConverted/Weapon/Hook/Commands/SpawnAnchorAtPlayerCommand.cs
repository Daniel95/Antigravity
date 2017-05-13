using IoCPlus;
using UnityEngine;

public class SpawnAnchorAtPlayerCommand : Command {

    [Inject] private PlayerModel playerModel;

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        Debug.Log(hookProjectileRef.Get());
        Debug.Log(hookProjectileRef.Get().Transform);

        hookRef.Get().AddAnchor(
            playerModel.Player.transform.position, 
            hookProjectileRef.Get().Transform
        );
    }
}
