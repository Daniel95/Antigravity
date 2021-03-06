﻿using IoCPlus;
using UnityEngine;

public class HookProjectileMoveTowardsNextAnchorCommand : Command {

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> hookProjectileMoveTowardsRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;
    [Inject] private Ref<IHook> hookRef;

    [Inject] private HookProjectileMoveTowardsNextAnchorCompletedEvent hookProjectileMoveTowardsNextAnchorCompletedEvent;

    protected override void Execute() {
        Vector2 nextPoint = hookRef.Get().Anchors[hookRef.Get().Anchors.Count - 2].position;
        hookProjectileMoveTowardsRef.Get().StartMovingToTarget(nextPoint, hookProjectileMoveTowardsNextAnchorCompletedEvent);
    }

}