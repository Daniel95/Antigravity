using UnityEngine;
using System.Collections;
using System;
using IoCPlus;

public class RevivedStateView : View, IRevivedState, ITriggerer {

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    public bool IsInPosition { set { isInPosition = value; } }

    [Inject] private CharacterUpdateLineDestinationEvent characterUpdateLineDestinationEvent;

    [SerializeField] private GameObject gun;

    [SerializeField]
    private int startDirectionRayLength = 50;

    [SerializeField]
    private int launchDelayWhenRespawning = 20;

    private MoveTowardsView moveTowards;
    private LookAt lookAt;
    private bool isInPosition; //the first time we hit a checkpoint, we will be moving towards the center of it, once we reached the center we are in position and can fire ourselfes.
    private bool canFire;

    public override void Initialize() {
        base.Initialize();
        StartCoroutine(DelayLaunchingInput());
    }

    public override void Dispose() {
        base.Dispose();

        if (StopTrigger != null) {
            StopTrigger();
        }
    }

    private void Awake() {
        lookAt = gun.GetComponent<LookAt>();
        moveTowards = GetComponent<MoveTowardsView>();
        GetComponent<CharacterVelocityView>();
    }

    IEnumerator DelayLaunchingInput() {
        int framesCounter = launchDelayWhenRespawning;

        while (framesCounter < 0 || !isInPosition) {
            framesCounter--;
            yield return new WaitForFixedUpdate();
        }

        canFire = true;
    }

    public void StartMovingToCenterCheckPoint(Vector2 checkPointPosition) {
        moveTowards.ReachedDestination += ReachedPosition;
        moveTowards.StartMoving(checkPointPosition);
    }

    private void ReachedPosition() {
        isInPosition = true;

        if (ActivateTrigger != null) {
            ActivateTrigger();
        }
    }

    public void Aim(Vector2 direction){
        characterUpdateLineDestinationEvent.Dispatch((Vector2)transform.position + (direction * startDirectionRayLength));

        lookAt.UpdateLookAt((Vector2)transform.position + direction);
    }
}