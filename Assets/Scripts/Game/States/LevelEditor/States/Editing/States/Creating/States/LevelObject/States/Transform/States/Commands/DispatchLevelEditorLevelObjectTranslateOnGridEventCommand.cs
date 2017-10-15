using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorLevelObjectTranslateOnGridEventCommand : Command {

    [Inject] private LevelEditorLevelObjectTranslateOnGridEvent levelObjectTranslateOnGridEvent;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        levelObjectTranslateOnGridEvent.Dispatch(gridPosition);
    }

}
