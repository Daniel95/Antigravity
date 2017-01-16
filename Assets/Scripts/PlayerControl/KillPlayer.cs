using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    private Checkpoint _checkpoint;

    private void Start()
    {
        _checkpoint = GetComponent<Checkpoint>();

        GetComponent<GetKilled>().Die += PlayerDies;
    }

    /// <summary>
    /// check if the player checked a checkpoint, of not, reload the scene.
    /// </summary>
    private void PlayerDies()
    {
        if (_checkpoint.CheckPointReached)
        {
            _checkpoint.Revive();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}