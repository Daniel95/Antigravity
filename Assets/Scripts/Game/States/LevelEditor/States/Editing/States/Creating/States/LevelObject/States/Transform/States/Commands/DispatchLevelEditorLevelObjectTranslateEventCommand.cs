using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorLevelObjectTranslateEventCommand : Command {

    [Inject] private LevelEditorLevelObjectTranslateEvent levelObjectTranslateEvent;

    [InjectParameter] private Vector2 worldPosition;

    protected override void Execute() {
        levelObjectTranslateEvent.Dispatch(worldPosition);
    }

}
