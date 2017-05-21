﻿using IoCPlus;
using UnityEngine;

public class PCInputContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<EnablePCInputCommand>(true);

        On<LeaveContextSignal>()
            .Do<EnablePCInputCommand>(false);
    }

}