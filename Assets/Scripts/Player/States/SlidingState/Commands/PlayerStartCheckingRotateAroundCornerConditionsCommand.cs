﻿using IoCPlus;
using UnityEngine;

public class PlayerStartCheckingRotateAroundCornerConditionsCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterSliding> playerSlidingRef;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        Vector2 cornerPosition = collider.transform.position;
        playerSlidingRef.Get().StartCheckingRotateAroundCornerConditions(cornerPosition);
    }

}
