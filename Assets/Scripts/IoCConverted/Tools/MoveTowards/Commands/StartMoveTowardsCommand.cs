using IoCPlus;
using UnityEngine;

public class StartMoveTowardsCommand : Command {

    [Inject] private Ref<IMoveTowards> moveTowardsRef;

    [InjectParameter] private Vector2 destination;

    protected override void Execute() {
        moveTowardsRef.Get().StartMoving(destination);
    }
}
