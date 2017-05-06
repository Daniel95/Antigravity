using IoCPlus;
using UnityEngine;

public class InstantiatePlayerCommand : Command {

    [Inject] private IContext context;

    [Inject] private PlayerModel playerModel;
    
    private const string PLAYER_PREFAB_PATH = "Characters/Player";

    protected override void Execute() {
        View playerViewPrefab = Resources.Load<View>(PLAYER_PREFAB_PATH);

        if (GameObject.FindGameObjectWithTag(Tags.Start) == null) {
            Debug.Log("Cant find start, aborting");
            Abort();
            return;
        }

        Debug.Log("instantiating player");

        Vector2 startPosition = GameObject.FindGameObjectWithTag(Tags.Start).transform.position;
        playerModel.Player = context.InstantiateView(playerViewPrefab).gameObject;
        //playerModel.player = GameObject.Instantiate(playerPrefab, startPosition, Quaternion.identity);
        playerModel.Player.transform.position = startPosition;
        Debug.Log("instantiated the player");

    }
}
