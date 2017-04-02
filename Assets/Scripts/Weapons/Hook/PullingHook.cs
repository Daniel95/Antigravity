using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingHook : HookTypeBase {

    private ControlVelocity velocity;
    
    private void Awake()
    {
        velocity = GetComponent<ControlVelocity>();
    }

    /*
    protected override void Hooked(int hookedLayer)
    {
        base.Hooked(hookedLayer);

        if (hookedLayer != HookAbleLayers.PullSurface) return;

        Vector2 newDirection = (HookProjectileGObj.transform.position - transform.position).normalized;

        Vector2 velocityDirection = velocity.GetVelocityDirection();

        newDirection.x *= Mathf.Abs(velocityDirection.x);
        newDirection.y *= Mathf.Abs(velocityDirection.y);

        velocity.SetDirection(newDirection);

        Canceled();
    }*/
}
