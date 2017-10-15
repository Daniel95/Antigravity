﻿using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorLevelObjectTranslateOffGridEventCommand : Command {

    [Inject] private LevelEditorLevelObjectTranslateOffGridEvent levelObjectTranslateOffGridEvent;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMovedEventParameter;

    protected override void Execute() {
        Vector2 screenPosition = swipeMovedEventParameter.Position;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        levelObjectTranslateOffGridEvent.Dispatch(worldPosition);
    }

}
