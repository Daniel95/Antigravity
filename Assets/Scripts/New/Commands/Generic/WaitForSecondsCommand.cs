using IoCPlus;
using UnityEngine;
using System.Collections;

public class WaitForSecondsCommand : Command<float> {

	protected override void ExecuteOverTime(float seconds) {
        CoroutineHelper.Start(Wait(seconds));
	}

    private IEnumerator Wait(float seconds) {
        yield return new WaitForSeconds(seconds);
        Release();
    }

}