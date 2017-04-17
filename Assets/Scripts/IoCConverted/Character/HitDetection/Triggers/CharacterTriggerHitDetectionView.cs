using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriggerHitDetectionView : View, ICharacterTriggerHitDetection {

    public List<string> CurrentTriggerTags { get { return currentTriggerTags; } }

    [Inject] private Ref<ICharacterTriggerHitDetection> characterHitTriggerRef;

    [Inject] private TriggerEnter2DEvent onTriggerEnter2DEvent;
    [Inject] private TriggerStay2DEvent onTriggerStay2DEvent;
    [Inject] private TriggerExit2DEvent onTriggerExit2DEvent;

    private List<string> currentTriggerTags = new List<string>();

    public override void Initialize() {
        characterHitTriggerRef.Set(this);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        currentTriggerTags.Add(collision.tag);
        onTriggerEnter2DEvent.Dispatch(collision);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        onTriggerStay2DEvent.Dispatch(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        currentTriggerTags.Remove(collision.tag);
        onTriggerExit2DEvent.Dispatch(collision);
    }
}
