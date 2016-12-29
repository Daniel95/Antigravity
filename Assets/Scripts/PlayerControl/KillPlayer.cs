using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    private Checkpoint checkpoint;

    private GetKilled getKilled;

    private void Start()
    {
        checkpoint = GetComponent<Checkpoint>();

        getKilled = GetComponent<GetKilled>();
        getKilled.die += PlayerDies;
    }

    private void PlayerDies()
    {
        //check if the player checked a checkpoint, of not, reload the scene
        if (checkpoint.CheckPointReached)
        {
            checkpoint.Revive();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}