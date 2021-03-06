﻿using IoCPlus;
using UnityEngine;

public class DebugLogCollision2DCommand : Command {

    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        Debug.Log("Collision : " + collision.transform.name, collision.gameObject);
    }

}
