using IoCPlus;
using UnityEngine;

public class StopMoveTowardsCommand : Command {

    [Inject] private Ref<IMoveTowards> moveTowardsRef;

    protected override void Execute() {
        moveTowardsRef.Get().StopMoving();
    }
}
