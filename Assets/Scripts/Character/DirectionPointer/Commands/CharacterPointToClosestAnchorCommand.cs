using IoCPlus;
using UnityEngine;

public class CharacterPointToClosestAnchorCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<ICharacterDirectionPointer> directionPointerRef;

    [Inject] private CharacterPointToDirectionEvent characterPointToDirectionEvent;

    protected override void Execute() {
        Vector2 lookDirection = (hookRef.Get().Anchors[0].position - weaponRef.Get().Owner.transform.position).normalized;
        directionPointerRef.Get().PointToDirection(lookDirection);
    }
}

