using HedgehogTeam.EasyTouch;
using IoCPlus;

public class SpawnTileAtTapPositionCommand : Command {

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    [InjectParameter] private Gesture gesture;

    protected override void Execute() {
        levelEditorCreatingInputRef.Get().SpawnTileAtScreenPosition(gesture.position);
    }

}
