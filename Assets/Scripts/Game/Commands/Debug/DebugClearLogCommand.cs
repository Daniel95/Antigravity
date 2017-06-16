using IoCPlus;
using System;
using System.Reflection;

public class DebugClearLogCommand : Command {

    protected override void Execute() {
        Type logEntries = Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
        MethodInfo clearMethod = logEntries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }
}
