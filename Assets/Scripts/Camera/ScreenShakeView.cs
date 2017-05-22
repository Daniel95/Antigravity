using UnityEngine;
using System.Collections;
using IoCPlus;

public class ScreenShakeView : View, IScreenShake {

    [Inject] private Ref<IScreenShake> screenShakeRef;

    [SerializeField] private int minShakeTime = 2;
    [SerializeField] private int maxShakeTime = 35;
    [SerializeField] private float shakeStrength = 0.04f;

    private bool shaking;

    public override void Initialize() {
        screenShakeRef.Set(this);
    }

    public void StartScreenShake() {
        //stop the previous shake if we are already shaking
        if (shaking) {
            StopAllCoroutines();
        }

        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();

        shaking = true;

        //choose a random number between minshake and maxshake, that is the amount of times we are going to shake
        float shakeTimes = Random.Range(minShakeTime, maxShakeTime);

        //decrement the shakeTime until its 0 or below, then we stop shaking
        while (shakeTimes >= 0)
        {
            //decrement the amount of shakes we have left
            shakeTimes--;

            //says the position is startpos x/y incremented by a random number between minShakeStrength and maxShakeStrength
            transform.position = new Vector3(transform.position.x + Random.Range(-shakeStrength, shakeStrength), transform.position.y + Random.Range(-shakeStrength, shakeStrength), transform.position.z);

            yield return waitForFixedUpdate;
        }

        shaking = false;
    }
}
