using IoCPlus;

public class FinishSelectionFieldCommand : Command {

    [Inject] private Ref<ITileSpawner> tileSpawnerRef;

    protected override void Execute() {
        tileSpawnerRef.Get().FinishSelectionField();
    }

}
