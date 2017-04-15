using IoCPlus;
using UnityEngine;

public class StartAimLineCommand : Command {

    [Inject] private Ref<IAimLine> aimLineRef;

    [InjectParameter] private Vector3 destination;

    protected override void Execute() {
        aimLineRef.Get().StartAimLine(destination);
    }
}
