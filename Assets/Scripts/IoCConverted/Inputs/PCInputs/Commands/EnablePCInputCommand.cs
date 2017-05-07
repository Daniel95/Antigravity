using IoCPlus;
using UnityEngine;

public class EnablePCInputCommand : Command<bool> {

    [Inject] private Ref<IPCInput> pcInputRef;

    protected override void Execute(bool enable) {
        pcInputRef.Get().EnableInput(enable);
    }
}
