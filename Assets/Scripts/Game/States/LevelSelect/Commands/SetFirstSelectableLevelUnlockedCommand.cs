using IoCPlus;
using UnityEngine;

public class SetFirstSelectableLevelUnlockedCommand : Command {

    [Inject] private Ref<ISelectableLevelField> selectableLevelFieldRef;

    protected override void Execute() {
        foreach (ISelectableLevel selectableLevel in selectableLevelFieldRef.Get().SelectableLevels.Values) {
            if (selectableLevel.LevelNumber == 1) {
                selectableLevel.IncreaseLevelProgressStateTo(LevelProgressState.Unlocked);
                return;
            }
        }
    }
}
