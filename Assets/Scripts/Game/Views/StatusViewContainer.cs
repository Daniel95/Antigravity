using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class StatusViewContainer : View, IStatusViewContainer {

    [Inject] IContext context;

    [Inject] private Ref<IStatusViewContainer> statusViewContainerRef;

    private Dictionary<string, View> statusViewsByKey = new Dictionary<string, View>();

    public void AddStatusView<T>() where T : View {
        string name = typeof(T).ToString();
        GameObject gameObjectStatusViewGameObject = new GameObject() {
            name = name,
        };
        gameObjectStatusViewGameObject.transform.SetParent(transform);
        T statusView = gameObjectStatusViewGameObject.AddComponent<T>();
        statusViewsByKey.Add(name, statusView);
        context.AddView(statusView);
    }

    public void RemoveStatusView<T>() where T : View {
        string name = typeof(T).ToString();
        View statusView = statusViewsByKey[name];
        statusView.Destroy();
        statusViewsByKey.Remove(name);
    }

    public override void Initialize() {
        base.Initialize();
        statusViewContainerRef.Set(this);
    }

}
