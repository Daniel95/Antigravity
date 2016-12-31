using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBase : MonoBehaviour {

    [SerializeField]
    private List<GameObject> objsToTrigger;

    //the triggers with a reference to its gameobject, so that we can check if its gameobject is active before we activate the triggers
    private Dictionary<GameObject, List<ITriggerable>> objectsWithTriggers = new Dictionary<GameObject, List<ITriggerable>>();

    private void Start()
    {
        //save all the triggers in the objsToTrigger, so we can activate them later
        foreach (GameObject obj in objsToTrigger) {

            List<ITriggerable> triggersInObj = new List<ITriggerable>();

            foreach (ITriggerable trigger in obj.GetComponents<ITriggerable>())
            {
                triggersInObj.Add(trigger);
            }

            objectsWithTriggers.Add(obj, triggersInObj);
        }
    }

    protected void ActivateTriggers()
    {
        foreach (GameObject obj in objectsWithTriggers.Keys)
        {
            //only activate the triggers on the object if the object is active and its parents are active
            if (obj.activeInHierarchy)
            {
                //use each key in the dict to get the value, which is an List<Itrigger>      
                //then loop through each Itrigger in the list and activate it.
                for (int i = 0; i < objectsWithTriggers[obj].Count; i++)
                {
                    objectsWithTriggers[obj][i].TriggerActivate();
                }
            }
        }
    }

    protected void StopTriggers()
    {
        foreach (GameObject obj in objectsWithTriggers.Keys)
        {
            //only activate the triggers on the object if the object is active and its parents are active
            if (obj.activeInHierarchy)
            {
                //use each key in the dict to get the value, which is an List<Itrigger>
                //then loop through each Itrigger in the list and activate it.
                for (int i = 0; i < objectsWithTriggers[obj].Count; i++)
                {
                    objectsWithTriggers[obj][i].TriggerStop();
                }
            }
        }
    }
}
