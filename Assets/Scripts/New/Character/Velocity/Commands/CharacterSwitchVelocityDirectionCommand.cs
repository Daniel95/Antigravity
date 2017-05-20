﻿using IoCPlus;
using UnityEngine;

public class CharacterSwitchVelocityDirectionCommand : Command {

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    protected override void Execute() {
        controlVelocityRef.Get().SwitchVelocityDirection();
    }
}
