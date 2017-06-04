using IoCPlus;
using UnityEngine;

public class AbortIfColliderIsPlayerCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        if(collider.gameObject == playerStatus.Player) {
            Abort();
        }
    }
}
