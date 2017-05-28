using IoCPlus;
using UnityEngine;

public class CharacterPointToClosestAnchorCommand : Command {

    [Inject] private Ref<ICharacterDirectionPointer> directionPointerRef;
    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        Vector2 lookDirection = (hookRef.Get().Anchors[0].position - hookRef.Get().Owner.transform.position).normalized;
        directionPointerRef.Get().PointToDirection(lookDirection);
    }
}

