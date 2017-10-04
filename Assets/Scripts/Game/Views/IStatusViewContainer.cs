public interface IStatusViewContainer {

    void AddStatusView<T>() where T : StatusView;
    void RemoveStatusView<T>() where T : StatusView;

}
