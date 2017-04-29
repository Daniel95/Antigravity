using IoCPlus;

public class PullingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<HookProjectileIsAttachedEvent>();


        /*
            public void Hooked(int hookedLayer) {

        if (hookedLayer != HookModel.HookAbleLayers.PullSurface) return;

        Vector2 newDirection = (hookModel.HookProjectileGameObject.transform.position - transform.position).normalized;

        Vector2 velocityDirection = velocity.GetVelocityDirection();

        newDirection.x *= Mathf.Abs(velocityDirection.x);
        newDirection.y *= Mathf.Abs(velocityDirection.y);

        velocity.SetDirection(newDirection);

        cancelHookEvent.Dispatch();
    }
        */
    }
}

