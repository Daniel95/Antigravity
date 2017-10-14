using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorLevelObjectTranslateOffGridEventCommand : Command {

    [Inject] private LevelEditorLevelObjectTranslateOffGridEvent levelObjectTranslateOffGridEvent;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = LevelEditorGridHelper.ScreenToNodePosition(screenPosition);
        levelObjectTranslateOffGridEvent.Dispatch(worldPosition);
    }

}
