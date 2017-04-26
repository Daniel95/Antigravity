﻿using IoCPlus;
using UnityEngine;

public class AbortIfGameObjectIsNotPlayerCommand : Command {

    [Inject] private PlayerModel playerModel;

    [InjectParameter] private GameObject gameobject;

    protected override void Execute() {
        if(gameobject != playerModel.player) {
            Abort();
        }

        Collider2D coll;
    }
}
