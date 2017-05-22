public interface ICharacterSpeed  {

    float MaxSpeedMultiplier { get; }
    float SpeedChangeDivider { get; }
    float MaxSpeedChange{ get; }
    float SpeedBoostValue { get; }
    float ReturnSpeed { get; }
    int ChangeSpeedCDStartValue { get; }
    int ChangeSpeedCDCounter { get; set; }
    bool ChangeSpeedCDIsActive { get; }

    float CalculateNewSpeed(float currentSpeed, float amount, float neutralValue);

    void StartChangeSpeedCdCounter();
}
