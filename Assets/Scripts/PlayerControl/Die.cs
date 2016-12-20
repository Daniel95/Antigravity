using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour {

    [SerializeField]
    private string killerTag = "Killer";

    private bool enteredKillerTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(killerTag)) {
            enteredKillerTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //only check when necessary
        if (enteredKillerTrigger)
        {
            if (collision.CompareTag(killerTag))
            {
                enteredKillerTrigger = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (enteredKillerTrigger) {
            GetKilled();
        }
    }

    private void GetKilled()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
