using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriggerHitDetectionView : View, ICharacterTriggerHitDetection {

    [Inject] private Ref<ICharacterTriggerHitDetection> characterHitTriggerRef;

    public IEnumerable<string> CurrentTriggerTags { get { return currentTriggerTags; } }

    [Inject] private OnTriggerEnter2DEvent onTriggerEnter2DEvent;
    [Inject] private OnTriggerStay2DEvent onTriggerStay2DEvent;
    [Inject] private OnTriggerExit2DEvent onTriggerExit2DEvent;

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
