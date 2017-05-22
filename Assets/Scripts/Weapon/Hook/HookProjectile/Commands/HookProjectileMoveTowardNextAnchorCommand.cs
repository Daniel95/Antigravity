using IoCPlus;
using UnityEngine;

public class HookProjectileMoveTowardNextAnchorCommand : Command {

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> moveTowardsRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;
    [Inject] private Ref<IHook> hookRef;

    [Inject] private HookProjectileMoveTowardsNextAnchorEvent hookProjectileMoveTowardsNextAnchorEvent;

    protected override void Execute() {
        Vector2 nextPoint = hookRef.Get().Anchors[(hookRef.Get().Anchors.Count - 1) - hookProjectileRef.Get().AnchorsIndex].position;
        hookProjectileRef.Get().AnchorsIndex++;
        moveTowardsRef.Get().StartMoving(nextPoint, hookProjectileMoveTowardsNextAnchorEvent);
    }
}