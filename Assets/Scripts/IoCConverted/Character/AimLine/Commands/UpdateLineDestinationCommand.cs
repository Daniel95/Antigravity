using IoCPlus;
using UnityEngine;

public class UpdateLineDestinationCommand : Command {

    [Inject] private Ref<IAimLine> aimLineRef;

    [InjectParameter] private Vector3 destination;

    protected override void Execute() {
        aimLineRef.Get().LineDestination = destination;
    }
}
