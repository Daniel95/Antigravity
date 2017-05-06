using IoCPlus;
using UnityEngine;

public class EnablePCInputCommand : Command<bool> {

    [Inject] private Ref<IPCInput> pcInputRef;

    protected override void Execute(bool enable) {
        Debug.Log("need pcInputRef: ");
        Debug.Log(pcInputRef.Get());
        
        pcInputRef.Get().EnableInput(enable);
    }
}
