using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHitDetectionView : View, ICollisionHitDetection {

    public List<string> CurrentCollisionTags { get { return hittingCollisionTags; } }

    [Inject] protected CollisionEnter2DEvent OnCollisionEnter2DEvent;
    [Inject] protected CollisionStay2DEvent OnCollisionStay2DEvent;
    [Inject] protected CollisionExit2DEvent OnCollisionExit2DEvent;

    private List<string> hittingCollisionTags = new List<string>();

    private int lastContactPoints;

    public void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Player hits " + collision.transform.name, gameObject);
        OnCollisionEnter2DEvent.Dispatch(gameObject, collision);
        lastContactPoints = collision.contacts.Length;
    }

    public void OnCollisionStay2D(Collision2D collision) {
        if(lastContactPoints != collision.contacts.Length) {

            OnContactPointChange(collision);
            lastContactPoints = collision.contacts.Length;
        }
        OnCollisionStay2DEvent.Dispatch(gameObject, collision);
    }

    public void OnCollisionExit2D(Collision2D collision) {
        OnCollisionExit2DEvent.Dispatch(gameObject, collision);
    }

    protected virtual void OnContactPointChange(Collision2D collision) {
        Debug.Break();
        Debug.Log("Scripted oncollision");
        OnCollisionEnter2DEvent.Dispatch(gameObject, collision);
    }

}
