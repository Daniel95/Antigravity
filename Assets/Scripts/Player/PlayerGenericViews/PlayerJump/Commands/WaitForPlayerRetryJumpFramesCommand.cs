using UnityEngine;
using System.Collections;
using IoCPlus;

public class WaitForPlayerRetryJumpFramesCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterJump> playerJumpRef;

    protected override void ExecuteOverTime() {
        CoroutineHelper.Start(Wait(playerJumpRef.Get().RetryJumpWaitFrames));
    }

    private IEnumerator Wait(int frames) {
        for (int i = 0; i < frames; i++) {
            yield return new WaitForEndOfFrame();
        }
        Release();
    }
}
