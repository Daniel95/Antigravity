﻿using IoCPlus;
using UnityEngine;

public class AbortIfPlayerCollisionDirectionIsZeroCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    protected override void Execute() {
        if(playerCollisionDirectionRef.Get().CollisionDirection == Vector2.zero) {
            Abort();
        }
    }

}
