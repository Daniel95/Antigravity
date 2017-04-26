using IoCPlus;
using UnityEngine;

public class DispatchPlayerTriggerExit2DEvent : Command {

    [Inject] private PlayerTriggerExit2DEvent playerTriggerExit2DEvent;

    private GameObject gameObject;
    private Collider2D collider;

    protected override void Execute() {
        playerTriggerExit2DEvent.Dispatch(gameObject, collider);
    }
}
