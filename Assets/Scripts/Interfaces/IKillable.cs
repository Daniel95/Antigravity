using UnityEngine;

public interface IKillable
{
    void EnteringKillingTrigger(string killerTag);

    void ExitingKillingTrigger(string killerTag);
}
