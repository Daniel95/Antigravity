using IoCPlus;

public class GameLevelContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<LevelUIContext>()
            .GotoState<LevelContext>();

        On<PlayerCollisionEnter2DEvent>()
            .Do<AbortIfCollisionTagIsNotTheSameAsCommand>(Tags.Finish)
            .Do<AddCurrentSceneToCompletedLevelsCommand>()
            .Do<SaveGameStateCommand>()
            .Dispatch<GoToNextSceneEvent>();

    }
}
