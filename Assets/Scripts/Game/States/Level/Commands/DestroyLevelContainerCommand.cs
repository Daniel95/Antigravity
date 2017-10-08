using IoCPlus;
using UnityEngine;

public class DestroyLevelContainerCommand : Command {

    [Inject] private LevelContainerStatus levelContainerStatus;

    protected override void Execute() {
        Object.Destroy(levelContainerStatus.LevelContainer.gameObject);
    }

}
