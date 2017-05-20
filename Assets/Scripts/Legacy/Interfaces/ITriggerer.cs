using UnityEngine;
using System;

public interface ITriggerer
{
    Action ActivateTrigger { get; set; }

    Action StopTrigger { get; set; }
}
