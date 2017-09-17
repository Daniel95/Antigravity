using IoCPlus;

public class ListenerView : View, IListener {

    public Signal Signal { get { return signal; } set { signal = value; } }

    private Signal signal;

    public void DispatchSignal() {
        signal.Dispatch();
    }

}
