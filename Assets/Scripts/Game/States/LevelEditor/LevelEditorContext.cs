using IoCPlus;

public class LevelEditorContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<TapEvent>()
            .Do<SpawnTileAtTapPositionCommand>();

        On<SwipeMovedEvent>()
            .Do<SpawnTileAtSwipePositionCommand>();

    }

}
