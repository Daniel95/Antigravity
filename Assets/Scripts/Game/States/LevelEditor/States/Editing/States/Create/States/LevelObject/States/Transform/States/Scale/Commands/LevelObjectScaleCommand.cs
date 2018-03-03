using IoCPlus;
using UnityEngine;

public class LevelObjectScaleCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    [InjectParameter] private SwipeMovedOnWorldEvent.Parameter levelEditorSwipeMovedOnWorldEventParameter;

    protected override void Execute() {
        Vector2 screenDelta = levelEditorSwipeMovedOnWorldEventParameter.Delta;
        Vector2 previousScreenPosition = levelEditorSwipeMovedOnWorldEventParameter.Position - levelEditorSwipeMovedOnWorldEventParameter.Delta;
        Vector2 previousWorldPosition = Camera.main.ScreenToWorldPoint(previousScreenPosition);
        Vector2 currentWorldPosition = Camera.main.ScreenToWorldPoint(levelEditorSwipeMovedOnWorldEventParameter.Position);

        Vector2 worldDelta = currentWorldPosition - previousWorldPosition;
        float distance = worldDelta.sqrMagnitude;

        Vector2 offsetToSelectedLevelObject = levelEditorSwipeMovedOnWorldEventParameter.Position - (Vector2)selectedLevelObjectRef.Get().GameObject.transform.position;

        float input = Vector2.Angle(screenDelta, offsetToSelectedLevelObject) / -90.0f + 1.0f;
        float scaleStrength = input * distance;
        Vector2 scale = new Vector2(scaleStrength, scaleStrength);

        selectedLevelObjectRef.Get().Scale(scale);
    }

}
