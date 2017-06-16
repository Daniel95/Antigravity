using IoCPlus;
using UnityEngine;

public class SetSelectableLevelUnlockedCommand : Command<int> {

    [Inject] private Ref<ISelectableLevelField> selectableLevelFieldRef;

    protected override void Execute(int levelNumber) {
        foreach (ISelectableLevel selectableLevel in selectableLevelFieldRef.Get().SelectableLevels.Values) {
            if (selectableLevel.LevelNumber == levelNumber) {
                selectableLevel.IncreaseLevelProgressStateTo(LevelProgressState.Unlocked);
                return;
            }
        }
    }
}
