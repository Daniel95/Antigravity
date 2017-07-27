using IoCPlus;
using UnityEngine;

public class SetFollowCameraTargetCommand : Command {

    [Inject] private PlayerStatus playerStatus;
    [Inject] private Ref<IFollowCamera> followCameraRef;

    protected override void Execute() {
        followCameraRef.Get().SetTarget(playerStatus.Player.transform);
    }
}
