using IoCPlus;
using UnityEngine;

public class InstantiatePlayerCommand : Command {

    [Inject] private IContext context;

    [Inject] private PlayerStatus playerStatus;

    private const string PLAYER_PREFAB_PATH = "Characters/Player";

    protected override void Execute() {
        View playerViewPrefab = Resources.Load<View>(PLAYER_PREFAB_PATH);
        View playerView = context.InstantiateView(playerViewPrefab);
        playerStatus.Player = playerView.gameObject;
    }
}
