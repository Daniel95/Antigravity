using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class AbortIfTriggerIsNotLayersIndexes : Command<List<int>> {

    [InjectParameter] private Collider2D collider;

    protected override void Execute(List<int> layers) {
        bool layerIsFound = false;

        layers.ForEach(x => {
            if (collider.gameObject.layer == x) {
                layerIsFound = true;
            }
        });

        if (!layerIsFound) {
            Abort();
        }
    }
}