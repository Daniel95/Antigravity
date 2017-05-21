using IoCPlus;
using UnityEngine;

public class DebugBreakCommand : Command {

    protected override void Execute() {
        Debug.Break();
    }
}
