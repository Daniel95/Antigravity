using IoCPlus;

public class PlayerContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiatePlayerCommand>()
            .AddContext<InputContext>()
            .AddContext<WeaponContext>()
            .AddContext<PlayerStateContext>()
            .AddContext<CharacterContext>()
            .Do<EnableInputCommand>(true)
            .Do<EnableWeaponCommand>(true)
            .Do<EnableJumpCommand>(true);

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.Finish)
            .Do<AddCurrentSceneToCompletedLevelsCommand>()
            .Do<SaveGameStateCommand>()
            .Dispatch<GoToNextSceneEvent>();

        On<CharacterDieEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<ChooseAndDispatchPlayerDiesEventCommand>();

        On<RespawnPlayerEvent>()
            .Do<InstantiatePlayerCommand>()
            .Do<StartScreenShakeCommand>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerCollisionEnter2DEvent>();

        On<CollisionStay2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerCollisionStay2DEvent>();

        On<CollisionExit2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerCollisionExit2DEvent>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerTriggerEnter2DEvent>();

        On<TriggerStay2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerTriggerStay2DEvent>();

        On<TriggerExit2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<DispatchPlayerTriggerExit2DEvent>();
    }
}