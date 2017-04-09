using IoCPlus;
using UnityEngine;

public class InstantiatePlayerCommand : Command {

    [Inject] private PlayerModel playerModel;
    
    private const string PLAYER_PREFAB_PATH = "Characters/Player";

    protected override void Execute() {
        GameObject playerPrefab = Resources.Load<GameObject>(PLAYER_PREFAB_PATH);

        if (GameObject.FindGameObjectWithTag(Tags.Start) == null) {
            Debug.Log("Cant find start, aborting");
            Abort();
            return;
        }

        Vector2 startPosition = GameObject.FindGameObjectWithTag(Tags.Start).transform.position;
        playerModel.player = GameObject.Instantiate(playerPrefab, startPosition, Quaternion.identity);
        playerModel.player.transform.position = startPosition;
    }
}
