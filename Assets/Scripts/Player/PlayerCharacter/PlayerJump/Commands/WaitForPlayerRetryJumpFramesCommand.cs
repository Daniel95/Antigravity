using UnityEngine;
using System.Collections;
using IoCPlus;

public class WaitForPlayerRetryJumpTimeCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterJump> playerJumpRef;

    protected override void ExecuteOverTime() {
        CoroutineHelper.Start(Wait(playerJumpRef.Get().RetryJumpWaitTime));
    }

    private IEnumerator Wait(float time) {
        yield return new WaitForSecondsRealtime(time);
        Release();
    }
}
