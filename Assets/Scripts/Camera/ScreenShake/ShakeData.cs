using System;

[Serializable]
public struct ShakeData {
    public float duration;
    public float randomDurationOffset;
    public float targetStrength;
    public float randomTargetStrengthOffset;
}