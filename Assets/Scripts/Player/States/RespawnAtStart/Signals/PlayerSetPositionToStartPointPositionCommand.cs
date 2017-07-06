using IoCPlus;
using UnityEngine;

public class PlayerSetPositionToStartPointPositionCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    protected override void Execute() {
        Vector2 startPointPosition = GameObject.FindGameObjectWithTag(Tags.StartPoint).transform.position;

        if (GameObject.FindGameObjectWithTag(Tags.StartPoint) == null) {
            Debug.LogWarning("Can't set player to start because start is null.");
            Abort();
            return;
        }

        playerStatus.Player.transform.position = startPointPosition;
    }
}
