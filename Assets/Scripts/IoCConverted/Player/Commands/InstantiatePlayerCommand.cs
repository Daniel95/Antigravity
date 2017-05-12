using IoCPlus;
using UnityEngine;

public class InstantiatePlayerCommand : Command {

    [Inject] private IContext context;

    [Inject] private PlayerModel playerModel;
    
    private const string PLAYER_PREFAB_PATH = "Character/Player";

    protected override void Execute() {
        View playerViewPrefab = Resources.Load<View>(PLAYER_PREFAB_PATH);

        if (GameObject.FindGameObjectWithTag(Tags.Start) == null) {
            Debug.Log("Cant find start, aborting");
            Abort();
            return;
        }

        Vector2 startPosition = GameObject.FindGameObjectWithTag(Tags.Start).transform.position;
        View playerView = context.InstantiateView(playerViewPrefab);
        GameObject playerGO = playerView.gameObject;
        playerGO.transform.position = startPosition;
        playerModel.Player = playerGO;
    }
}
