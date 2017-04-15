using IoCPlus;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayerView : View {

    [Inject] private ActivateRevivedStateEvent activateRevivedStateEvent;

    public static Action PlayerGettingKilled;

    [SerializeField] 
    private GameObject dieEffect;

    private CheckPointView checkpoint;

    private void Start()
    {
        checkpoint = GetComponent<CheckPointView>();

        GetComponent<CharacterDieView>().Die += PlayerDies;
    }

    /// <summary>
    /// check if the player checked a checkpoint, of not, reload the scene.
    /// </summary>
    private void PlayerDies()
    {
        if (PlayerGettingKilled != null)
            PlayerGettingKilled();

        if (checkpoint.CheckPointReached) {
            Instantiate(dieEffect, transform.position, transform.rotation);
            activateRevivedStateEvent.Dispatch();
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}