using IoCPlus;
using UnityEngine;


public class InstantiatePlayerCommand : Command {

    [Inject] private PlayerModel playerModel;

    private const string PLAYER_PREFAB_PATH = "Characters/Player";

    protected override void Execute() {
        GameObject prefab = Resources.Load(PLAYER_PREFAB_PATH) as GameObject;

        playerModel.player = GameObject.Instantiate(prefab);
    }
}
