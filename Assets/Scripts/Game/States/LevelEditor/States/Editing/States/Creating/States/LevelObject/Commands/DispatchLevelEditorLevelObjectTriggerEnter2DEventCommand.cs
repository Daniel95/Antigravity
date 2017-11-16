using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorLevelObjectTriggerEnter2DEventCommand : Command {

    [Inject] private LevelEditorLevelObjectTriggerEnter2DEvent levelObjectTriggerEnter2DEvent;

    [InjectParameter] private GameObject gameObject;
    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        levelObjectTriggerEnter2DEvent.Dispatch(gameObject, collider);
    }

}
