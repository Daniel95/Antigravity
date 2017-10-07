using HedgehogTeam.EasyTouch;
using IoCPlus;
using UnityEngine;

public class AbortIfFingerTouchCountIsHigherThenCommand : Command<int> {

    protected override void Execute(int touchCount) {
        if(EasyTouch.GetTouchCount() > touchCount) {
            Abort();
        }
    }

}
