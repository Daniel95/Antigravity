using IoCPlus;

public interface IListener {

    Signal Signal { get; set; }

    void DispatchSignal();

}
