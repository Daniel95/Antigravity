using UnityEngine;

namespace IoCPlus {

    public class InstantiateViewInViewContainerCommand : Command<string> {

        [Inject] IContext context;

        [Inject] private ViewContainerStatus viewContainerStatus;

        protected override void Execute(string prefabPath) {
            View prefab = Resources.Load<View>(prefabPath);
            if (prefab == null) {
                Debug.LogWarning("Can't instantiate view prefab in ViewContainer as no prefab is found at given path '" + prefabPath + "'.");
                return;
            }
            View view = context.InstantiateView(prefab);
            view.transform.SetParent(viewContainerStatus.ViewContainer.transform);
        }

    }

}
