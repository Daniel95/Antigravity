using IoCPlus;
using UnityEngine;

public class StopSlowTimeCommand : Command {

    [Inject] private Ref<ISlowTime> slowTimeRef;

    protected override void Execute() {
        slowTimeRef.Get().StopSlowTime();
    }
}
