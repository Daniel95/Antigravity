﻿using IoCPlus;
using UnityEngine;

public class AbortIfCharacterIsNotStuckCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    protected override void Execute() {
        if (characterVelocityRef.Get().Velocity != Vector2.zero || characterVelocityRef.Get().CurrentSpeed == 0) {
            Abort();
        }
    }
}
