using IoCPlus;
using UnityEngine;

public class IgnoreLayerCollisionCommand : Command<string, string, bool> {

    protected override void Execute(string layer1Name, string layer2Name, bool ignore) {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(layer1Name), LayerMask.NameToLayer(layer2Name), ignore);
    }

}
