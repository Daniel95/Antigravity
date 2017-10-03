using IoCPlus;

public class CameraContainerView : View {

    public static CameraContainerView Instance { get { return GetInstance(); } }

    private static CameraContainerView instance;

    private static CameraContainerView GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<CameraContainerView>();
        }
        return instance;
    }

}
