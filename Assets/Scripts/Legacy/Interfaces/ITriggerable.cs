using UnityEngine;

public interface ITriggerable
{
    bool Triggered { get; set; }

    void TriggerActivate();

    void TriggerStop();
}
