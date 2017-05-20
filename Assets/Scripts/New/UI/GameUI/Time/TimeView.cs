using IoCPlus;
using UnityEngine;

public class TimeView : View, ITime {

    public bool IsPaused { get { return Time.timeScale <= 0; } }

    [Inject] private Ref<ITime> timeRef;

    public override void Initialize() {
        timeRef.Set(this);
    }

    public void PauseTime(bool pause) {
        Time.timeScale = pause ? 0 : 1;
    }
}
