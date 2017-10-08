using IoCPlus;
using UnityEngine;

public class AbortIfContextStateIsCommand<T> : Command where T : IContext {

    [Inject] private IContext context;

    protected override void Execute() {
        if (context.State.GetType() == typeof(T)) {
            Abort();
        }
    }

}
