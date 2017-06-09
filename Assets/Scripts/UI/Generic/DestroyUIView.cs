using IoCPlus;
using System;
using UnityEngine;

public class DestroyUIView : View {

    private AnimatedPanel animatedPanel;

    public override void Dispose() {
        if(animatedPanel == null) {
            Destroy();
            return;
        }

        PopOutAndDestroy(null);
    }

    public void PopOutAndDestroy(Action onPopOutCompleted = null) {
        animatedPanel.PopOut(onPopOutCompleted, true);
        this.RemoveViewComponent(true);
    }

    private void Awake() {
        animatedPanel = GetComponent<AnimatedPanel>();
    }
}
