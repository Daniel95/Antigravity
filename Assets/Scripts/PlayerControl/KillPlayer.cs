using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public static Action PlayerGettingKilled;

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
        if (PlayerGettingKilled != null)
            PlayerGettingKilled();

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