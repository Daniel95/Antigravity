using UnityEngine;
using System;

public interface ITriggerer
{
    Action activateTrigger { get; set; }

    Action stopTrigger { get; set; }
}
