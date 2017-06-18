using IoCPlus;
using System;
using UnityEngine;

public class CharacterJumpView : View, ICharacterJump {

    public float InstantJumpStrength { get { return jumpStrength; } }
    public float JumpSpeedBoost { get { return jumpSpeedBoost; } }
    public int RetryJumpWaitFrames { get { return retryJumpWaitFrames; } }

    [SerializeField] private float jumpStrength = 0.05f;
    [SerializeField] private float jumpSpeedBoost = 0.3f;
    [SerializeField] private int retryJumpWaitFrames = 10;

}
