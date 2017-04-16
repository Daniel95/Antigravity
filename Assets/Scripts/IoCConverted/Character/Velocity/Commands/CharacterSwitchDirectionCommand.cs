using IoCPlus;
using UnityEngine;

public class CharacterSwitchDirectionCommand : Command {

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    protected override void Execute() {
        controlVelocityRef.Get().SwitchDirection();
    }
}
