﻿using IoCPlus;
using UnityEngine;

public class SpawnHookProjectileAnchorCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookRef.Get().AddAnchor(
            hookProjectileRef.Get().Transform.position, 
            hookProjectileRef.Get().Transform
        );
    }
}
