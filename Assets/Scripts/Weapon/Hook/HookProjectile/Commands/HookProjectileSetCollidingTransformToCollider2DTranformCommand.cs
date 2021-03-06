﻿using IoCPlus;
using UnityEngine;

public class HookProjectileAddCollider2DGameObjectToCollidingGameObjectsCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        hookProjectileRef.Get().CollidingGameObjects.Add(collider.gameObject);
    }
}
