using IoCPlus;
using UnityEngine;

public class AddLevelViewCommand : Command {

    [Inject] private IContext context;

    protected override void Execute() {
        LevelView level = GameObject.FindObjectOfType<LevelView>();

        context.AddView(level);
    }
}
