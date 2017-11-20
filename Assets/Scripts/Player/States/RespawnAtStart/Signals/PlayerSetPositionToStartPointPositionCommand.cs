using IoCPlus;
using UnityEngine;

public class PlayerSetPositionToStartPointPositionCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    protected override void Execute() {
        GameObject startPointGameObject = GameObject.FindGameObjectWithTag(Tags.StartPoint);
        if (GameObject.FindGameObjectWithTag(Tags.StartPoint) == null) {
            Debug.LogWarning("Can't set player to start because start is null.");
            return;
        }

        Vector2 startPointPosition = startPointGameObject.transform.position;
        playerStatus.Player.transform.position = startPointPosition;
    }
}
