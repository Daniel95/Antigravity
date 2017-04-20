using IoCPlus;
using UnityEngine;

public class InstantiateGameObjectCommand : Command<string> {

    [Inject] private PlayerModel playerModel;

    protected override void Execute(string prefabPath) {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);

        GameObject.Instantiate(prefab, playerModel.player.transform.position, Quaternion.identity);
    }
}
