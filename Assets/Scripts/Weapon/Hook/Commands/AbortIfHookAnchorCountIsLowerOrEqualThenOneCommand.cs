﻿using IoCPlus;
using UnityEngine;

public class AbortIfHookAnchorCountIsLowerOrEqualThenOneCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        if (hookRef.Get().Anchors.Count <= 1) {
            Abort();
        }
    }
}
