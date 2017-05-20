﻿using IoCPlus;
using UnityEngine;

public class ChangeSpeedByAngleCommand : Command {

    [Inject] private PlayerModel playerModel;

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectile;

    [Inject] private CharacterTemporarySpeedChangeEvent characterTemporarySpeedChangeEvent;

    protected override void Execute() {
        float angleDifference =
            Mathf.Abs(Vector2.Dot((hookProjectile.Get().Transform.position - playerModel.Player.transform.position).normalized,
            characterVelocityRef.Get().GetVelocityDirection()));

        float speedChange = angleDifference * -1 + 1;

        characterTemporarySpeedChangeEvent.Dispatch(new CharacterTemporarySpeedChangeEvent.Parameter(speedChange, hookRef.Get().DirectionSpeedNeutralValue));
    }
}
