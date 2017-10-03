﻿using IoCPlus;
using UnityEngine;

public class AbortIfPlayerIsNotStuckCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        if (playerVelocityRef.Get().Velocity != Vector2.zero || playerVelocityRef.Get().CurrentSpeed == 0) {
            Abort();
        }
    }
}
