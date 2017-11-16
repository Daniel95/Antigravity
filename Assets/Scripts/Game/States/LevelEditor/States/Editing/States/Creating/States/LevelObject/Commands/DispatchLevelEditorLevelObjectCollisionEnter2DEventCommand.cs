using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorLevelObjectCollisionEnter2DEventCommand : Command {

    [Inject] private LevelEditorLevelObjectCollisionEnter2DEvent levelObjectCollisionEnter2DEvent;

    [InjectParameter] private GameObject gameObject;
    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        levelObjectCollisionEnter2DEvent.Dispatch(gameObject, collision);
    }

}
