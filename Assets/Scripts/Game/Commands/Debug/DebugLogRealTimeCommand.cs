using IoCPlus;
using UnityEngine;

public class DebugLogRealTimeCommand : Command<string> {

    protected override void Execute(string message) {
        Debug.Log("RealTime: " + Time.realtimeSinceStartup);
    }
}

