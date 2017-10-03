using IoCPlus;
using UnityEngine;

public class DispatchPlayerTriggerStay2DEvent : Command {

    [Inject] private PlayerTriggerStay2DEvent playerTriggerStay2DEvent;

    [InjectParameter] private GameObject gameObject;
    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        playerTriggerStay2DEvent.Dispatch(gameObject, collider);
    }
}
