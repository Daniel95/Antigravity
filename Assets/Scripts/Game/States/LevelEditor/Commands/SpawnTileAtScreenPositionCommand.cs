using IoCPlus;
using UnityEngine;

public class SpawnTileAtScreenPositionCommand : Command {

    [Inject] private Ref<ITileSpawner> tileSpawnerRef;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        tileSpawnerRef.Get().SpawnTileAtScreenPosition(screenPosition);
    }

}
