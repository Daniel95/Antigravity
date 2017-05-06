using IoCPlus;
using UnityEngine;

public class UpdateGrapplingStateCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocity;
    [Inject] private Ref<IHook> hookRef;

    [Inject] private CharacterPointToDirectionEvent characterPointToDirectionEvent;
    [Inject] private ActivateSlidingStateEvent activateSlidingStateEvent;
    [Inject] private CharacterTurnToNextDirectionEvent characterTurnToNextDirectionEvent;
    [Inject] private CharacterSetVelocityEvent characterSetVelocityEvent;

    protected override void Execute() {
        if (characterVelocity.Get().Velocity == Vector2.zero && characterVelocity.Get().CurrentSpeed != 0) {
            characterTurnToNextDirectionEvent.Dispatch();
            activateSlidingStateEvent.Dispatch();
        }

        characterPointToDirectionEvent.Dispatch(hookRef.Get().Destination);

        Vector2 velocity = characterVelocity.Get().GetVelocityDirection() * characterVelocity.Get().CurrentSpeed;
        characterSetVelocityEvent.Dispatch(velocity);
    }
}