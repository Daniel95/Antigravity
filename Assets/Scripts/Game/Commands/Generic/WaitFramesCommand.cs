using IoCPlus;
using UnityEngine;
using System.Collections;

public class WaitFramesCommand : Command<int> {

	protected override void ExecuteOverTime(int frames) {
        CoroutineHelper.Start(Wait(frames));
	}

    private IEnumerator Wait(int frames) {
        for (int i = 0; i < frames; i++) {
            yield return new WaitForEndOfFrame();
        }
        Release();
    }

}