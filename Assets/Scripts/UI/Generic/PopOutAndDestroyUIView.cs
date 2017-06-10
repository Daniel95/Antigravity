using IoCPlus;
using System;
using UnityEngine;

[RequireComponent(typeof(AnimatedPanel))]
public class PopOutAndDestroyUIView : View {

    private AnimatedPanel animatedPanel;

    public void PopOutAndDestroy(Action onPopOutCompleted = null) {
        animatedPanel.PopOut(onPopOutCompleted, true);
        this.RemoveViewComponent(true);
    }

    private void Awake() {
        animatedPanel = GetComponent<AnimatedPanel>();
    }
}
