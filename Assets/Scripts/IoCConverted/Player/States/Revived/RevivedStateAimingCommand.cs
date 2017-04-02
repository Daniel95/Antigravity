using IoCPlus;
using UnityEngine;

public class RevivedStateAimingCommand : Command {

    [Inject] private Ref<IRevivedState> revivedStateRef;

    [InjectParameter] private Vector3 direction;

    protected override void Execute() {
        revivedStateRef.Get().Aim(direction);
    }
}
