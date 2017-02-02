using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

    [SerializeField]
    private int minShakeTime = 2;

    [SerializeField]
    private int maxShakeTime = 35;

    [SerializeField]
    private float shakeStrength = 0.04f;

    private bool _shaking;

    private void OnEnable()
    {
        PlayerCharges.NotEnoughCharges += StartShake;
        KillPlayer.PlayerGettingKilled += StartShake;
    }

    private void OnDisable()
    {
        PlayerCharges.NotEnoughCharges -= StartShake;
        KillPlayer.PlayerGettingKilled -= StartShake;
    }

    public void StartShake() {
        //stop the previous shake if we are already shaking
        if (_shaking)
            StopAllCoroutines();

        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        _shaking = true;

        //save the initual position when when start shaking
        Vector3 startPos = transform.position;

        //choose a random number between minshake and maxshake, that is the amount of times we are going to shake
        float shakeTimes = Random.Range(minShakeTime, maxShakeTime);

        //decrement the shakeTime until its 0 or below, then we stop shaking
        while (shakeTimes >= 0)
        {
            //decrement the amount of shakes we have left
            shakeTimes--;

            //says the position is startpos x/y incremented by a random number between minShakeStrength and maxShakeStrength
            transform.position = new Vector3(startPos.x + Random.Range(-shakeStrength, shakeStrength), startPos.y + Random.Range(-shakeStrength, shakeStrength), startPos.z);

            yield return new WaitForFixedUpdate();
        }

        //reset the position to the position before we started shaking
        transform.position = startPos;

        _shaking = false;
    }
}
