using IoCPlus;
using UnityEngine;

public class PullingHookPullCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    protected override void Execute() {
        Vector2 newDirection = (hookRef.Get().HookProjectileGameObject.transform.position - hookRef.Get().Owner.transform.position).normalized;

        Vector2 velocityDirection = characterVelocityRef.Get().GetVelocityDirection();

        newDirection.x *= Mathf.Abs(velocityDirection.x);
        newDirection.y *= Mathf.Abs(velocityDirection.y);

        characterVelocityRef.Get().MoveDirection = newDirection;
    }
}
