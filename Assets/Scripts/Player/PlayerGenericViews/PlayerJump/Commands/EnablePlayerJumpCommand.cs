using IoCPlus;
using UnityEngine;

public class EnablePlayerJumpCommand : Command<bool> {

    [Inject] private PlayerJumpStatus playerJumpStatus;

    protected override void Execute(bool enable) {
        playerJumpStatus.JumpIsEnabled = enable;
    }
}