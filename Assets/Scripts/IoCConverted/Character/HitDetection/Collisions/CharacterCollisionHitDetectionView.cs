using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisionHitDetectionView : View {

    public List<string> CurrentCollisionTags { get { return currentCollisionTags; } }

    [Inject] private Ref<ICharacterTriggerHitDetection> characterHitTriggerRef;

    [Inject] private CollisionEnter2DEvent onCollisionEnter2DEvent;
    [Inject] private CollisionStay2DEvent onCollisionStay2DEvent;
    [Inject] private CollisionExit2DEvent onCollisionExit2DEvent;

    private List<string> currentCollisionTags = new List<string>();

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
