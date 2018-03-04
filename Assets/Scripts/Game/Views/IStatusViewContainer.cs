using IoCPlus;

public interface IStatusViewContainer {

    void AddStatusView<T>() where T : View;
    void RemoveStatusView<T>() where T : View;

}
