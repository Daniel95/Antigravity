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

        Vector2 startPosition = GameObject.FindGameObjectWithTag(Tags.Start).transform.position;
        context.AddView(playerViewPrefab);
        playerModel.Player = GameObject.Instantiate(playerViewPrefab.gameObject, startPosition, Quaternion.identity);
        playerModel.Player.transform.position = startPosition;
    }
}
