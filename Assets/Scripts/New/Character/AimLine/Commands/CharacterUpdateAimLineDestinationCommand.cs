using IoCPlus;
using UnityEngine;

public class CharacterUpdateAimLineDestinationCommand : Command {

    [Inject] private Ref<IAimDestination> shootRef;
    [Inject] private Ref<ICharacterAimLine> aimLineRef;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        Vector2 destination = shootRef.Get().GetDestinationPoint(direction);

        aimLineRef.Get().UpdateAimLineDestination(destination);
    }
}
