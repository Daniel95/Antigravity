using IoCPlus;
using UnityEngine;

public class CheckPointView : View {

    [Inject] private ActivateRevivedStateEvent activateRevivedStateEvent;

    private Vector2 _lastCheckpointLocation;

    private bool _checkpointReached;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Tags.CheckPoint))
        {
            _checkpointReached = true;

            _lastCheckpointLocation = collision.transform.position;

            transform.position = _lastCheckpointLocation;

            activateRevivedStateEvent.Dispatch();
        }
    }

    public bool CheckPointReached
    {
        get { return _checkpointReached; }
    }
}
