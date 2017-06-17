using IoCPlus;
using UnityEngine;

public class GameObjectDestroyViewCommand : Command {

    [InjectParameter] private GameObject gameObject;

    protected override void Execute() {
        View view = gameObject.GetComponent<View>();

        if(view == null) {
            Debug.LogWarning("GameObject " + gameObject.name + " being destroyed does not have a View component");
            return;
        }

        view.Destroy();
    }
}
