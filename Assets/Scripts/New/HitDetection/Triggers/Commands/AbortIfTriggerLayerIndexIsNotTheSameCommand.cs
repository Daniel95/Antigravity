using IoCPlus;
using UnityEngine;

public class AbortIfTriggerLayerIndexIsNotTheSameCommand : Command<int> {

    [InjectParameter] private Collider2D collider;

    protected override void Execute(int layer) {
        Debug.Log(layer);
        if (collider.gameObject.layer != layer) {
            Abort();
        }
    }
}