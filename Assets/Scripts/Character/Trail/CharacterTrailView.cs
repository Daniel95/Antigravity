using IoCPlus;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class CharacterTrailView : View, ICharacterTrail {

    private TrailRenderer trailRendererComponent;

    public void EnableTrail() {
        trailRendererComponent.enabled = true;
    }

    public void DisableTrail() {
        trailRendererComponent.enabled = false;
    }

    private void Awake() {
        trailRendererComponent = GetComponent<TrailRenderer>();
    }
}
