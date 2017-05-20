using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBase : MonoBehaviour {

    [SerializeField]
    private List<GameObject> objsToActivate;

    //the triggers with a reference to its gameobject, so that we can check if its gameobject is active before we activate the triggers
    private Dictionary<GameObject, List<ITriggerable>> _objectsTriggersToActivate = new Dictionary<GameObject, List<ITriggerable>>();

    [SerializeField]
    private List<GameObject> objsToStop;

    //the triggers with a reference to its gameobject, so that we can check if its gameobject is active before we activate the triggers
    private Dictionary<GameObject, List<ITriggerable>> _objectsTriggersToStop = new Dictionary<GameObject, List<ITriggerable>>();

    private void Start()
    {
        //save all the triggers to activate in the objectsTriggersToActivate, so we can activate them later
        foreach (GameObject obj in objsToActivate) {

            List<ITriggerable> triggersInObj = new List<ITriggerable>();

            foreach (ITriggerable trigger in obj.GetComponents<ITriggerable>())
            {
                triggersInObj.Add(trigger);
            }

            _objectsTriggersToActivate.Add(obj, triggersInObj);
        }

        //save all the triggers to stop in the objectsTriggersToStop, so we can stop them later
        foreach (GameObject obj in objsToStop)
        {

            List<ITriggerable> triggersInObj = new List<ITriggerable>();

            foreach (ITriggerable trigger in obj.GetComponents<ITriggerable>())
            {
                triggersInObj.Add(trigger);
            }

            _objectsTriggersToStop.Add(obj, triggersInObj);
        }
    }

    /// <summary>
    /// Activates all saved triggers that are activeInHierarchy.
    /// </summary>
    protected void ActivateTriggers()
    {
        foreach (GameObject obj in _objectsTriggersToActivate.Keys)
        {
            //only activate the triggers on the object if the object is active and its parents are active
            if (obj.activeInHierarchy)
            {
                //use each key in the dict to get the value, which is an List<Itrigger>      
                //then loop through each Itrigger in the list and activate it.
                for (int i = 0; i < _objectsTriggersToActivate[obj].Count; i++)
                {
                    //only activate the trigger, if the object isn't already triggered
                    if (!_objectsTriggersToActivate[obj][i].Triggered)
                    {
                        _objectsTriggersToActivate[obj][i].Triggered = true;
                        _objectsTriggersToActivate[obj][i].TriggerActivate();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Stops all saved triggers that are activeInHierarchy.
    /// </summary>
    protected void StopTriggers()
    {
        foreach (GameObject obj in _objectsTriggersToStop.Keys)
        {
            //only activate the triggers on the object if the object is active and its parents are active
            if (obj.activeInHierarchy)
            {
                //use each key in the dict to get the value, which is an List<Itrigger>
                //then loop through each Itrigger in the list and activate it.
                for (int i = 0; i < _objectsTriggersToStop[obj].Count; i++)
                {
                    //only stop the trigger, if the object was previously triggered
                    if (_objectsTriggersToStop[obj][i].Triggered)
                    {
                        _objectsTriggersToStop[obj][i].Triggered = false;
                        _objectsTriggersToStop[obj][i].TriggerStop();
                    }
                }
            }
        }
    }
}
