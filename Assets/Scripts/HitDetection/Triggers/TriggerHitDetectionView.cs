using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHitDetectionView : View, ITriggerHitDetection {

    public List<string> HittingTriggerTags { get { return hittingTriggerTags; } }

    [Inject] private Ref<ITriggerHitDetection> triggerHitDetectionRef;

    [Inject] private TriggerEnter2DEvent onTriggerEnter2DEvent;
    [Inject] private TriggerStay2DEvent onTriggerStay2DEvent;
    [Inject] private TriggerExit2DEvent onTriggerExit2DEvent;

    private List<string> hittingTriggerTags = new List<string>();

    public override void Initialize() {
        triggerHitDetectionRef.Set(this);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        hittingTriggerTags.Add(collision.tag);
        onTriggerEnter2DEvent.Dispatch(gameObject, collision);
    }

    public void OnTriggerStay2D(Collider2D collision) {
        onTriggerStay2DEvent.Dispatch(gameObject, collision);
    }

    public void OnTriggerExit2D(Collider2D collision) {
        hittingTriggerTags.Remove(collision.tag);
        onTriggerExit2DEvent.Dispatch(gameObject, collision);
    }
}
