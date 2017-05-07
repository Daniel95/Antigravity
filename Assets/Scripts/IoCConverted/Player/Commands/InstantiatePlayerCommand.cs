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
        Debug.Log("spawn player");


        GameObject player = GameObject.Instantiate(playerViewPrefab.gameObject, startPosition, Quaternion.identity);
        player.transform.position = startPosition;
        playerModel.Player = player;
        Debug.Log("Init player");
        Debug.Log(playerModel.Player.activeInHierarchy);

        (context as Context).AddView(playerViewPrefab);
        //context.AddView(playerViewPrefab);
    }
}


/*
    protected override void Execute() {
        View playerViewPrefab = Resources.Load<View>(PLAYER_PREFAB_PATH);

        if (GameObject.FindGameObjectWithTag(Tags.Start) == null) {
            Debug.Log("Cant find start, aborting");
            Abort();
            return;
        }

        Vector2 startPosition = GameObject.FindGameObjectWithTag(Tags.Start).transform.position;
        Debug.Log("spawn player");


        GameObject player = GameObject.Instantiate(playerViewPrefab.gameObject, startPosition, Quaternion.identity);
        player.transform.position = startPosition;
        playerModel.Player = player;
        Debug.Log("Init player");
        Debug.Log(playerModel.Player.activeInHierarchy);

        (context as Context).AddView(playerViewPrefab);
        //context.AddView(playerViewPrefab);
    }
*/
