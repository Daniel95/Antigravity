using HedgehogTeam.EasyTouch;
using IoCPlus;

public class LevelEditorSpawnTileAtTapPositionCommand : Command {

    [Inject] private Ref<ILevelEditorCreating> levelEditorCreatingRef;

    [InjectParameter] private Gesture gesture;

    protected override void Execute() {
        levelEditorCreatingRef.Get().SpawnTileAtScreenPosition(gesture.position);
    }

}
