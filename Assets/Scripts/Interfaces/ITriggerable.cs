using UnityEngine;

public interface ITriggerable
{
    bool triggered { get; set; }

    void TriggerActivate();

    void TriggerStop();
}
