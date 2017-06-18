using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHitDetectionView : View, ITriggerHitDetection {

    public List<string> HittingTriggerTags { get { return hittingTriggerTags; } }

    [Inject] private TriggerEnter2DEvent onTriggerEnter2DEvent;
    [Inject] private TriggerStay2DEvent onTriggerStay2DEvent;
    [Inject] private TriggerExit2DEvent onTriggerExit2DEvent;

    private List<string> hittingTriggerTags = new List<string>();

    public void OnTriggerEnter2D(Collider2D collider) {
        hittingTriggerTags.Add(collider.tag);
        onTriggerEnter2DEvent.Dispatch(gameObject, collider);
    }

    public void OnTriggerStay2D(Collider2D collider) {
        onTriggerStay2DEvent.Dispatch(gameObject, collider);
    }

    public void OnTriggerExit2D(Collider2D collider) {
        hittingTriggerTags.Remove(collider.tag);
        onTriggerExit2DEvent.Dispatch(gameObject, collider);
    }
}
