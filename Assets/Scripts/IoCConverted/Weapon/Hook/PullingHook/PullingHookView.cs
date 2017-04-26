using IoCPlus;
using UnityEngine;

public class PullingHookView : View, IPullingHook {

    [Inject] private HookModel hookModel;

    [Inject] private Ref<IPullingHook> pullingHookRef;

    private CharacterVelocityView velocity;

    public override void Initialize() {
        pullingHookRef.Set(this);
    }

    public void Hooked(int hookedLayer) {

        if (hookedLayer != HookModel.HookAbleLayers.PullSurface) return;

        Vector2 newDirection = (hookModel.HookProjectileGameObject.transform.position - transform.position).normalized;

        Vector2 velocityDirection = velocity.GetVelocityDirection();

        newDirection.x *= Mathf.Abs(velocityDirection.x);
        newDirection.y *= Mathf.Abs(velocityDirection.y);

        velocity.SetDirection(newDirection);

        cancelHookEvent.Dispatch();
    }

    private void Awake() {
        velocity = GetComponent<CharacterVelocityView>();
    }
}