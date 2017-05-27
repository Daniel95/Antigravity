using IoCPlus;
using UnityEngine;

public class DebugLogMessageCommand : Command<string> {

    protected override void Execute(string message) {
        Debug.Log(message + " f." + Time.frameCount);
    }
}
