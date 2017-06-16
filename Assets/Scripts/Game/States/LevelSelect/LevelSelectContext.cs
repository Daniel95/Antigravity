using IoCPlus;

public class LevelSelectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<DestroySelectLevelFieldsCommand>()
            .Do<GenerateSelectableLevelFieldsCommand>()
            .Do<SetSelectableLevelUnlockedCommand>(1)
            .Do<SetSelectableLevelUnlockedCommand>(7)
            .Do<UnlockNeighboursOfFinishedSelectableLevelsCommand>()
            .Do<ApplySelectableLevelValuesCommand>()
            .Do<SetCameraBoundsCommand>()
            .Do<EnableFollowCameraCommand>(false)
            .Do<EnableDragCameraCommand>(true);

        On<EnterContextSignal>()
            .Do<AbortIfLastLevelIsZeroCommand>()
            .Do<FixateOnSelectableLevelWithLastLevelNumberCommand>();
    }

}
