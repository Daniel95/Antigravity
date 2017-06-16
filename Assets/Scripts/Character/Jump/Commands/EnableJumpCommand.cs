using IoCPlus;
using UnityEngine;

public class EnableJumpCommand : Command<bool> {

    [Inject] private JumpStatus jumpStatus;

    protected override void Execute(bool enable) {
        jumpStatus.JumpIsEnabled = enable;
    }
}