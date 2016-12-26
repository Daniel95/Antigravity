using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBase : MonoBehaviour {

    [SerializeField]
    private List<GameObject> triggerObjectsToActivate;

    private List<ITrigger> savedTriggers = new List<ITrigger>();

    private void Start()
    {
        //save all trigger on the triggerObjects, so we can activate them later
        for (int i = 0; i < triggerObjectsToActivate.Count; i++)
        {
            foreach (ITrigger trigger in triggerObjectsToActivate[i].GetComponents<ITrigger>())
            {
                savedTriggers.Add(trigger);
            }
        }
    }

    protected void ActivateTriggers()
    {
        for (int i = 0; i < savedTriggers.Count; i++)
        {
            savedTriggers[i].Activate();
        }
    }
}
