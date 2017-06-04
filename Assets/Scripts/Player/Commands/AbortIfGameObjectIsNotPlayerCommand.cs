using IoCPlus;
using UnityEngine;

public class AbortIfGameObjectIsNotPlayerCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    [InjectParameter] private GameObject gameobject;

    protected override void Execute() {
        if(gameobject != playerStatus.Player) {
            Abort();
        }
    }
}
