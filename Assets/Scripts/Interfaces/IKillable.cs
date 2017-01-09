using UnityEngine;

public interface IKillable
{
    void EnteringKillingTrigger(string _killerTag);

    void ExitingKillingTrigger(string _killerTag);
}
