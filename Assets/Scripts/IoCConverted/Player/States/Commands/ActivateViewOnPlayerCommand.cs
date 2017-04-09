using IoCPlus;
using UnityEngine;

public class ActivateViewOnPlayerCommand<T> : Command where T : View {

    [Inject] IContext context;

    [Inject] private PlayerModel playerModel;

    protected override void Execute() {
        View view = playerModel.player.GetComponent<T>();

        if(view == null) {
            Debug.LogWarning("A view in " + (context as Context) + " is null");
            Abort();
            return;
        }

        Debug.Log(context);
        Debug.Log(view);
        context.AddView(view, false);
    }
}
