using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class StatusViewContainer : View, IStatusViewContainer {

    [Inject] IContext context;

    [Inject] private Ref<IStatusViewContainer> statusViewContainerRef;

    private Dictionary<string, StatusView> statusViewsByKey = new Dictionary<string, StatusView>();

    public void AddStatusView<T>() where T : StatusView {
        string name = typeof(T).ToString();
        GameObject gameObject = new GameObject() {
            name = name,
        };
        gameObject.transform.SetParent(transform);
        T statusView = gameObject.AddComponent<T>();
        statusViewsByKey.Add(name, statusView);
        context.InstantiateView(statusView);
    }

    public void RemoveStatusView<T>() where T : StatusView {
        string name = typeof(T).ToString();
        StatusView statusView = statusViewsByKey[name];
        statusView.Destroy();
        statusViewsByKey.Remove(name);
    }

    public override void Initialize() {
        base.Initialize();
        statusViewContainerRef.Set(this);
    }

}
