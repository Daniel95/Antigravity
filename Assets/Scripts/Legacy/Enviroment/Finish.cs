using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {

    [SerializeField]
    private GameObject levelFinishedKeeper;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag(Tags.Player))
            return;

        GoToLevelSelect();
    }

    private void GoToLevelSelect()
    {
        GameObject keeperGameObject = GameObject.FindGameObjectWithTag(Tags.LevelFinishedKeeper); // FindObjectOfType(typeof(FinishedLevelKeeper)) as FinishedLevelKeeper;

        if (keeperGameObject != null)
        {
            keeperGameObject.GetComponent<FinishedLevelKeeper>().SaveCurrentLevelNumber();
        }
        else
        {
            Instantiate(levelFinishedKeeper).GetComponent<FinishedLevelKeeper>().SaveCurrentLevelNumber();
        }

        SceneManager.LoadScene("LevelSelect");
    }
}
