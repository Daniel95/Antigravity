using IoCPlus;
using UnityEngine;

public class InstantiateLevelContainerCommand : Command {

    [Inject] private LevelContainerStatus levelContainerStatus;

    protected override void Execute() {
        Transform levelContainer = new GameObject("LevelContainer").transform;
        levelContainerStatus.LevelContainer = levelContainer.transform;
    }

}