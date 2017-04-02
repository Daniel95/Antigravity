using IoCPlus;
using UnityEngine;

public class PlayerShootControlsTriggerView : View, ITriggerable {

    public bool Triggered { get; set; }

    [Inject] private EnableShootingInputEvent enableShootingInputEvent;

    [SerializeField] private GameObject player;

    public void TriggerActivate() {
        enableShootingInputEvent.Dispatch(true);
    }

    public void TriggerStop() {
        enableShootingInputEvent.Dispatch(false);
    }
}
