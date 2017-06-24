using IoCPlus;
using UnityEngine;

public class InstantiatePrefabOnPlayerPositionCommand : Command<string> {

    [Inject] private PlayerStatus playerStatus;

    protected override void Execute(string prefabPath) {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        Vector2 playerPosition = playerStatus.Player.transform.position;
        Object.Instantiate(prefab, playerPosition, new Quaternion());
    }

}
