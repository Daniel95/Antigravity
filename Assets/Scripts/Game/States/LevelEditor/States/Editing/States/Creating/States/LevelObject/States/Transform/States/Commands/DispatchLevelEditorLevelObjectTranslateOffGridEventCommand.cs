using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorLevelObjectTranslateOffGridEventCommand : Command {

    [Inject] private LevelEditorLevelObjectTranslateOffGridEvent levelObjectTranslateOffGridEvent;

    [InjectParameter] private Vector2 worldPosition;

    protected override void Execute() {
        levelObjectTranslateOffGridEvent.Dispatch(worldPosition);
    }

}
