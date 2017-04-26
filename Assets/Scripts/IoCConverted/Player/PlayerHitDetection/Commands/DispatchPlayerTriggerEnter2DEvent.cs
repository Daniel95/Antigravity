using IoCPlus;
using UnityEngine;

public class DispatchPlayerTriggerEnter2DEvent : Command {

    [Inject] private PlayerTriggerEnter2DEvent playerTriggerEnter2DEvent;

    private GameObject gameObject;
    private Collider2D collider;

    protected override void Execute() {
        playerTriggerEnter2DEvent.Dispatch(gameObject, collider);
    }
}
