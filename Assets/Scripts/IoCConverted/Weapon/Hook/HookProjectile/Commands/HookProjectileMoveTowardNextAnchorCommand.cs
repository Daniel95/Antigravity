using IoCPlus;
using UnityEngine;

public class HookProjectileMoveTowardNextAnchorCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;
    [Inject] private Ref<IHook> hookRef;

    [Inject] private StartMoveTowardsEvent startMoveTowardsEvent;

    protected override void Execute() {
        Vector2 nextPoint = hookRef.Get().Anchors[hookRef.Get().Anchors.Count - hookProjectileRef.Get().ReachedAnchorsIndex].position;
        hookProjectileRef.Get().ReachedAnchorsIndex++;
        startMoveTowardsEvent.Dispatch(nextPoint);
    }
}