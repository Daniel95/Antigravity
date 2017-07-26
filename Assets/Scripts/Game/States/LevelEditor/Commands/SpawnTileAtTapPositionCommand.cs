using HedgehogTeam.EasyTouch;
using IoCPlus;

public class SpawnTileAtTapPositionCommand : Command {

    [Inject] private Ref<ITileSpawner> tileSpawnerRef;

    [InjectParameter] private Gesture gesture;

    protected override void Execute() {
        tileSpawnerRef.Get().SpawnTileAtScreenPosition(gesture.position);
    }

}
