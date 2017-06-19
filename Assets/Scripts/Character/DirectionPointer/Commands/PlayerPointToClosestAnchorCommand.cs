using IoCPlus;
using UnityEngine;

public class PlayerPointToClosestAnchorCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject] private Ref<IHook> hookRef;
    [Inject(Label.Player)] private Ref<ICharacterDirectionPointer> playerDirectionPointerRef;

    protected override void Execute() {
        Vector2 lookDirection = (hookRef.Get().Anchors[0].position - weaponRef.Get().Owner.transform.position).normalized;
        playerDirectionPointerRef.Get().PointToDirection(lookDirection);
    }
}

