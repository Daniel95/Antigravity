using IoCPlus;

public class LevelSelectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<DestroySelectLevelFieldsCommand>()
            .Do<GenerateSelectableLevelFieldsCommand>()
            .Do<SetFirstSelectableLevelUnlockedCommand>()
            .Do<UnlockNeighboursOfFinishedSelectableLevelsCommand>()
            .Do<ApplySelectableLevelValuesCommand>()
            .Do<EnableFollowCameraCommand>(false)
            .Do<EnableDragCameraCommand>(true);

        On<EnterContextSignal>()
            .Do<AbortIfLastLevelIsZeroCommand>()
            .Do<FixateOnSelectableLevelWithLastLevelNumberCommand>();
    }

}
