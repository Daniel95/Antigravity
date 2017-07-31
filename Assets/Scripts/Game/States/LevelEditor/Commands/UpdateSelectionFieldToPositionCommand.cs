using IoCPlus;

public class UpdateSelectionFieldToSwipePositionCommand : Command {

    [Inject] private Ref<ITileSpawner> tileSpawnerRef;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        tileSpawnerRef.Get().UpdateSelectionField(swipeMoveEventParameter.Position);
    }

}
