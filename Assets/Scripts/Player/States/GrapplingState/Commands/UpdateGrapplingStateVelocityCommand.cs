using IoCPlus;
using UnityEngine;

public class UpdateGrapplingStateVelocityCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<IHook> hookRef;

    [Inject] private CharacterSetVelocityEvent characterSetVelocityEvent;

    protected override void Execute() {
        Vector2 velocity = characterVelocityRef.Get().GetVelocityDirection() * characterVelocityRef.Get().CurrentSpeed;
        characterSetVelocityEvent.Dispatch(velocity);
    }
}