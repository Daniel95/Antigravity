using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Tags.Player))
        {
            //check if the next scene exists
            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
