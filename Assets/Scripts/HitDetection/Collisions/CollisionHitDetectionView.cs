using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHitDetectionView : View, ICollisionHitDetection {

    public List<string> CurrentCollisionTags { get { return hittingCollisionTags; } }

    [Inject] private Ref<ICollisionHitDetection> collisionHitDetectionRef;

    [Inject] private CollisionEnter2DEvent onCollisionEnter2DEvent;
    [Inject] private CollisionStay2DEvent onCollisionStay2DEvent;
    [Inject] private CollisionExit2DEvent onCollisionExit2DEvent;

    private List<string> hittingCollisionTags = new List<string>();

    public override void Initialize() {
        collisionHitDetectionRef.Set(this);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        onCollisionEnter2DEvent.Dispatch(gameObject, collision);
    }

    public void OnCollisionStay2D(Collision2D collision) {
        onCollisionStay2DEvent.Dispatch(gameObject, collision);
    }

    public void OnCollisionExit2D(Collision2D collision) {
        onCollisionExit2DEvent.Dispatch(gameObject, collision);
    }
}
