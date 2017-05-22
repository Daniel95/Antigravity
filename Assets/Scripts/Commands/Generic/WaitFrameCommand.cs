using IoCPlus;
using UnityEngine;
using System.Collections;

public class WaitFrameCommand : Command {

	protected override void ExecuteOverTime() {
        CoroutineHelper.Start(Wait());
	}

    private IEnumerator Wait() {
        yield return new WaitForEndOfFrame();
        Release();
    }

}