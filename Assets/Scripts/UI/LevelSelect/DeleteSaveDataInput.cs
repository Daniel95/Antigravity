using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSaveDataInput : MonoBehaviour {

    public void DeleteSaveData()
    {
        Destroy(GameObject.FindGameObjectWithTag(Tags.LevelFinishedKeeper));
        LevelStatusPlayerPrefs.DeleteAllSavedData();
    }
}
