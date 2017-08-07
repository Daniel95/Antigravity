using HedgehogTeam.EasyTouch;
using IoCPlus;

public class SpawnTileAtTapPositionCommand : Command {

    [Inject] private Ref<ILevelEditorBuildingInput> levelEditorInputRef;

    [InjectParameter] private Gesture gesture;

    protected override void Execute() {
        levelEditorInputRef.Get().SpawnTileAtScreenPosition(gesture.position);
    }

}
