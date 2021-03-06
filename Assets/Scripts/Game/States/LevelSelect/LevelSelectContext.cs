﻿using IoCPlus;

public class LevelSelectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<LevelSelectUIContext>()
            .Do<DestroySelectLevelFieldsCommand>()
            .Do<GenerateSelectableLevelFieldsCommand>()
            .Do<SetSelectableLevelUnlockedCommand>(1)
            .Do<UnlockNeighboursOfFinishedSelectableLevelsCommand>()
            .Do<ApplySelectableLevelValuesCommand>()
            .Do<SetCameraBoundsCommand>()
            .Do<EnableCameraMoveInputCommand>(true);

        On<EnterContextSignal>()
            .Do<AbortIfLastLevelIsZeroCommand>()
            .Do<FixateOnSelectableLevelWithLastLevelNumberCommand>();

        On<LeaveContextSignal>()
            .Do<EnableCameraMoveInputCommand>(false);
    }

}
