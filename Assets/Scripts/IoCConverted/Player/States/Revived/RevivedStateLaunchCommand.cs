using IoCPlus;
using UnityEngine;

public class RevivedStateLaunchCommand : Command {

    [Inject] private Ref<IRevivedState> revivedStateRef;

    [InjectParameter] private Vector3 direction;

    protected override void Execute() {
        revivedStateRef.Get().Launch(direction);
    }
}
