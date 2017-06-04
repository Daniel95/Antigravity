using IoCPlus;
using UnityEngine;

public class SetMoveDirectionToHookProjectileDirectionCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;
    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    protected override void Execute() {
        Vector2 newDirection = (hookProjectileRef.Get().Transform.position - hookRef.Get().Owner.transform.position).normalized;
        characterVelocityRef.Get().SetMoveDirection(newDirection);
    }
}
