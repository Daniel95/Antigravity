using IoCPlus;

public class LevelSelectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<DestroySelectLevelFieldsCommand>()
            .Do<GenerateSelectableLevelFieldsCommand>()
            .Do<SetFirstSelectableLevelUnlockedCommand>()
            .Do<UnlockNeighboursOfFinishedSelectableLevelsCommand>()
            .Do<ApplySelectableLevelValuesCommand>();

        On<EnterContextSignal>()
            .Do<AbortIfLastLevelIsZeroCommand>();
    }

}
