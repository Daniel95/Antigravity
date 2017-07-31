using IoCPlus;
using UnityEngine;

public class StartSelectionFieldAtPositionCommand : Command {

    [Inject] private Ref<ITileSpawner> tileSpawnerRef;

    [InjectParameter] private Vector2 position;

    protected override void Execute() {
        tileSpawnerRef.Get().StartSelectionField(position);
    }

}
