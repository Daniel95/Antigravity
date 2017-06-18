using IoCPlus;
using System.Collections;
using UnityEngine;

public class CharacterSpeedView : View, ICharacterSpeed {

    public float MaxSpeedMultiplier { get { return maxSpeedMultiplier; } }
    public float SpeedChangeDivider { get { return speedChangeDivider; } }
    public float MaxSpeedChange { get { return maxSpeedChange; } }
    public float SpeedBoostValue { get { return speedBoostValue; } }
    public float ReturnSpeed { get { return returnSpeed; } }
    public int ChangeSpeedCDStartValue { get { return changeSpeedCdStartValue; } }
    public int ChangeSpeedCDCounter { get { return changeSpeedCDCounter; } set { changeSpeedCDCounter = value; } }
    public bool ChangeSpeedCDIsActive { get { return changeSpeedCDIsActive; } }

    [Inject] private Ref<ICharacterSpeed> characterSpeedRef;

    [Inject] private PlayerBoostSpeedEvent boostSpeedEvent;

    [SerializeField] private float maxSpeedMultiplier = 5f;
    [SerializeField] private float speedChangeDivider = 10f;
    [SerializeField] private float maxSpeedChange = 2;
    [SerializeField] private float speedBoostValue = 0.25f;
    [SerializeField] private float returnSpeed = 0.005f;
    [SerializeField] private int changeSpeedCdStartValue = 60;

    private int changeSpeedCDCounter = -1;
    private bool changeSpeedCDIsActive;

    public override void Initialize() {
        characterSpeedRef.Set(this);
    }

    public float CalculateNewSpeed(float currentSpeed, float amount, float neutralValue) {
        return currentSpeed + currentSpeed * (amount / neutralValue - 1) / (currentSpeed * speedChangeDivider);
    }

    public void StartChangeSpeedCdCounter() {
        StartCoroutine(ChangeSpeedCdCounter());
    }

    private IEnumerator ChangeSpeedCdCounter() {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        changeSpeedCDIsActive = true;
        changeSpeedCDCounter = changeSpeedCdStartValue;

        while (true) {
            changeSpeedCDCounter--;
            if (changeSpeedCDCounter < 0) {
                changeSpeedCDIsActive = false;
                yield break;
            }

            yield return waitForFixedUpdate;
        }
    }
}
