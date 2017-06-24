using IoCPlus;
using UnityEngine;

public class PlayerSetPositionToStartPositionCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    protected override void Execute() {
        Vector2 startPosition = GameObject.FindGameObjectWithTag(Tags.Start).transform.position;

        if (GameObject.FindGameObjectWithTag(Tags.Start) == null) {
            Debug.LogWarning("Can't set player to start because start is null.");
            Abort();
            return;
        }

        playerStatus.Player.transform.position = startPosition;
    }
}
