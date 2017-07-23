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
            .Dispatch<UnloadedCurrentSceneEvent>()
            .OnAbort<DispatchLoadNextSceneEventCommand>();

        On<UnloadedCurrentSceneEvent>()
            .Do<LoadNextSceneOverTimeCommand>()
            .Do<DispatchGoToSceneCompletedEventCommand>();
    }

}