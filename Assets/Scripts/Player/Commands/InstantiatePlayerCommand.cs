using IoCPlus;
using UnityEngine;

public class InstantiatePlayerCommand : Command {

    [Inject] private IContext context;

    [Inject] private PlayerStatus playerStatus;

    private const string PLAYER_PREFAB_PATH = "Characters/Player";

    protected override void Execute() {
        View playerViewPrefab = Resources.Load<View>(PLAYER_PREFAB_PATH);

        if (GameObject.FindGameObjectWithTag(Tags.Start) == null) {
            Debug.LogWarning("Cant find start in level for player to spawn at.");
            Abort();
            return;
        }

        Vector2 startPosition = GameObject.FindGameObjectWithTag(Tags.Start).transform.position;
        View playerView = context.InstantiateView(playerViewPrefab);
        GameObject playerGO = playerView.gameObject;
        playerGO.transform.position = startPosition;
        playerStatus.Player = playerGO;
    }
}
