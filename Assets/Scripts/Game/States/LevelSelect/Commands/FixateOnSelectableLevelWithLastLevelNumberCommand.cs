using IoCPlus;
using UnityEngine;

public class FixateOnSelectableLevelWithLastLevelNumberCommand : Command {

    [Inject] private LevelStatus levelStatus;

    [Inject] private Ref<ICamera> cameraRef;
    [Inject] private Ref<ISelectableLevelField> selectableLevelFieldRef;

    protected override void Execute() {
        ISelectableLevel selectableLevel = selectableLevelFieldRef.Get().GetSelectableLevelByLevelNumber(levelStatus.LastLevelNumber);
        cameraRef.Get().Position = selectableLevel.WorldPosition;
    }
}
