using IoCPlus;
using UnityEngine;

public class LevelEditorLevelObjectScaleCommand : Command {

    [InjectParameter] private LevelEditorSwipeMovedOnWorldEvent.Parameter levelEditorSwipeMovedOnWorldEventParameter;

    protected override void Execute() {
        Vector2 screenDelta = levelEditorSwipeMovedOnWorldEventParameter.Delta;
        Vector2 previousScreenPosition = levelEditorSwipeMovedOnWorldEventParameter.Position - levelEditorSwipeMovedOnWorldEventParameter.Delta;
        Vector2 previousWorldPosition = Camera.main.ScreenToWorldPoint(previousScreenPosition);
        Vector2 currentWorldPosition = Camera.main.ScreenToWorldPoint(levelEditorSwipeMovedOnWorldEventParameter.Position);

        Vector2 worldDelta = currentWorldPosition - previousWorldPosition;
        float distance = worldDelta.sqrMagnitude;

        Vector2 offsetToSelectedLevelObject = levelEditorSwipeMovedOnWorldEventParameter.Position - (Vector2)LevelEditorSelectedLevelObjectStatus.LevelObject.transform.position;
        Vector2 directionToSelectedLevelObject = offsetToSelectedLevelObject.normalized;
        Vector2 direction = VectorHelper.Abs(directionToSelectedLevelObject);

        float scaleValue = Vector2.Angle(screenDelta, offsetToSelectedLevelObject) / -90.0f + 1.0f;

        // = Vector2.Dot(screenDelta, offsetToSelectedLevelObject);

        Vector3 scale = (scaleValue * direction) * distance;

        Debug.Log("__________");
        Debug.Log("screenDelta " + screenDelta);
        Debug.Log("worldDelta " + worldDelta);
        Debug.Log("scaleValue " + scaleValue);
        Debug.Log("direction " + direction);
        Debug.Log("distance " + distance);
        Debug.Log("scale " + scale);

        LevelEditorSelectedLevelObjectStatus.LevelObject.transform.localScale += scale;
    }

}
