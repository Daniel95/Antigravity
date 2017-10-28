using IoCPlus;
using UnityEngine;

public class EnablePlayerJumpStatusCommand : Command<bool> {

    [Inject] private PlayerJumpStatus playerJumpStatus;

    protected override void Execute(bool enable) {
        playerJumpStatus.Enabled = enable;
    }
}