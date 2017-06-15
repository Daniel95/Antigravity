using IoCPlus;

public class LoadingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AbortIfCurrentSceneIsNotALevelCommand>()
            .Do<SetLastLevelToCurrentSceneCommand>();

        On<EnterContextSignal>()
            .Do<AbortIfCurrentSceneIsSceneCommand>(Scenes.Main)
            .Do<UnloadCurrentSceneOverTimeCommand>()
            .Dispatch<LoadNextSceneEvent>()
            .OnAbort<DispatchLoadNextSceneEventCommand>();

        On<LoadNextSceneEvent>()
            .Do<LoadNextSceneOverTimeCommand>()
            .Do<DispatchGoToSceneCompletedEventCommand>();
    }

}