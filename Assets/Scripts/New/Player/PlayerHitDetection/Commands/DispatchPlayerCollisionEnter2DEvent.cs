using IoCPlus;
using UnityEngine;

public class DispatchPlayerCollisionEnter2DEvent : Command {

    [Inject] private PlayerCollisionEnter2DEvent playerCollisionEnter2DEvent;

    [InjectParameter] private GameObject gameObject;
    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        playerCollisionEnter2DEvent.Dispatch(gameObject, collision);
    }
}
