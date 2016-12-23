using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    private CheckPoint checkPoint;

    private GetKilled getKilled;

    private void Start()
    {
        checkPoint = GetComponent<CheckPoint>();

        getKilled = GetComponent<GetKilled>();
        getKilled.die += PlayerDies;
    }

    private void PlayerDies()
    {
        //check if the player checked a checkpoint, of not, reload the scene
        if (checkPoint.CheckPointReached)
        {
            checkPoint.Revive();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}