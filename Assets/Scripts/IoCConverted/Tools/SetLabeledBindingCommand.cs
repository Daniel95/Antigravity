using IoCPlus;
using UnityEngine;

public class SetLabeledBindingCommand<T> : Command<GameObject> {

    [Inject] private Ref<T> interfaceRef;
    [Inject] private IContext context;

    protected override void Execute(GameObject gameObject) {
        interfaceRef.Set(gameObject.GetComponent<T>());
    }
}