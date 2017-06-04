﻿using IoCPlus;
using UnityEngine;

public class ActivateViewOnPlayerCommand<T> : Command where T : View {

    [Inject] IContext context;

    [Inject] private PlayerStatus playerStatus;

    protected override void Execute() {
        View view = playerStatus.Player.GetComponent<T>();

        if(view == null) {
            Debug.LogWarning("A view in " + (context as Context) + " is null");
            Abort();
            return;
        }

        context.AddView(view, false);
    }
}
