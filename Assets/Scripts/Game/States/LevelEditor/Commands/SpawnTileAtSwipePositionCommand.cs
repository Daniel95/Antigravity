using IoCPlus;
using UnityEngine;

public class SpawnTileAtSwipePositionCommand : Command {

    [Inject] private Ref<ITileSpawner> tileSpawnerRef;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        tileSpawnerRef.Get().SpawnTileAtScreenPosition(swipeMoveEventParameter.Position);
    }

}
