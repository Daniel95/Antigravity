using IoCPlus;
using UnityEngine;

public class CharacterJumpView : View, ICharacterJump {

    public float InstantJumpStrength { get { return instantJumpStrength; } }
    public float JumpSpeedBoost { get { return jumpSpeedBoost; } }
    public float RetryJumpWaitTime { get { return retryJumpWaitTime; } }

    [SerializeField] private float instantJumpStrength = 0.05f;
    [SerializeField] private float jumpSpeedBoost = 0.3f;
    [SerializeField] private float retryJumpWaitTime = 0.05f;

}
