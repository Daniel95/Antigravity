using IoCPlus;
using UnityEngine;

public class DispatchPlayerTriggerExit2DEvent : Command {

    [Inject] private PlayerTriggerExit2DEvent playerTriggerExit2DEvent;

    [InjectParameter] private GameObject gameObject;
    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        playerTriggerExit2DEvent.Dispatch(gameObject, collider);
    }
}
