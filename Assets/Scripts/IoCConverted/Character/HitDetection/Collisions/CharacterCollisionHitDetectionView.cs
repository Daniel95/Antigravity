using IoCPlus;
using UnityEngine;

public class CharacterCollisionHitDetectionView : View {

    [Inject] private OnCollisionEnter2DEvent onCollisionEnter2DEvent;
    [Inject] private OnCollisionStay2DEvent onCollisionStay2DEvent;
    [Inject] private OnCollisionExit2DEvent onCollisionExit2DEvent;

    private void OnCollisionEnter2D(Collision2D collision) {
        onCollisionEnter2DEvent.Dispatch(collision);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        onCollisionStay2DEvent.Dispatch(collision);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        onCollisionExit2DEvent.Dispatch(collision);
    }
}
