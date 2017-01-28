using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedLevelKeeper : MonoBehaviour {

    public string LevelName { get; private set; }

    public void SaveCurrentLevelNumber()
    {
        LevelName = SceneManager.GetActiveScene().name;
    }
}
