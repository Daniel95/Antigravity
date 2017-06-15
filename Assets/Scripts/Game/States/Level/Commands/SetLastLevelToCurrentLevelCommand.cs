using UnityEngine;
using System.Collections;
using IoCPlus;

public class SetLastLevelToCurrentLevelCommand : Command {

    [Inject] private LevelStatus levelStatus;
    [Inject] private SceneStatus sceneStatus;

    protected override void Execute() {
        if()

        sceneStatus.currentScene 
    }
}
