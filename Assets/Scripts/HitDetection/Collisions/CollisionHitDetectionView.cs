using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHitDetectionView : View, ICollisionHitDetection {

    public List<string> CurrentCollisionTags { get { return hittingCollisionTags; } }

    [Inject] protected CollisionEnter2DEvent OnCollisionEnter2DEvent;
    [Inject] protected CollisionStay2DEvent OnCollisionStay2DEvent;
    [Inject] protected CollisionExit2DEvent OnCollisionExit2DEvent;

    private List<string> hittingCollisionTags = new List<string>();

    public void OnCollisionEnter2D(Collision2D collision) {
        OnCollisionEnter2DEvent.Dispatch(gameObject, collision);
    }

    public void OnCollisionStay2D(Collision2D collision) {
        OnCollisionStay2DEvent.Dispatch(gameObject, collision);
    }

    public void OnCollisionExit2D(Collision2D collision) {
        OnCollisionExit2DEvent.Dispatch(gameObject, collision);
    }

}
