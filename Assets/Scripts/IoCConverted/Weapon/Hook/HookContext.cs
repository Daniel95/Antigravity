using IoCPlus;
using System.Collections.Generic;

public class HookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GrapplingHookStartedEvent>();
        Bind<CancelGrapplingHookEvent>();

        Bind<Ref<IHook>>();
        Bind<Ref<IGrapplingHook>>();
        Bind<Ref<IPullingHook>>();

        On<EnterContextSignal>();

        On<CancelGrapplingHookEvent>()
            .Do<StopSlowTimeCommand>();

        On<FireWeaponEvent>()
            .Do<AbortIfHookStateIsNotActive>(HookState.Inactive)
            .Do<DispatchShootHookEventCommand>()
            .OnAbort<DispatchHookPullBackEventCommand>();

        On<ShootHookEvent>()
            .Do<SetHookStateCommand>(HookState.BusyShooting)
            .Do<ActivateHookProjectileCommand>()
            .Do<SpawnAnchorAtPlayerCommand>()
            .Do<ActivateHoopRopeCommand>()
            .Do<SetHookedLayerCommand>(0)
            .Do<HookProjectileGoToDestinationCommand>();

        On<PullBackHookEvent>()
            .Do<AbortIfHookStatesAreActive>(new List<HookState>() {
                HookState.Inactive,
                HookState.BusyPullingBack })
            .Do<SetHookStateCommand>(HookState.BusyPullingBack);




        //todo
        On<ReachedDestinationEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>();

        //make special trigger event when the player touched something!
        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfTriggerIsNotLayerIndex>
            .Do<AbortIfTriggerIsNotLayersIndexes>

    }
}

/*
private void ShootHook(Vector2 destination, Vector2 spawnPosition) {

    hookProjectileScript.Attached = Hooked;
    hookProjectileScript.Canceled = Canceled;
}

//pulls the grappling hook back to the player, once it reached the player set it to inactive
private void PullBack() {

    List<Vector2> returnPoints = new List<Vector2>();
    foreach (Transform t in anchors) {
        returnPoints.Add(t.position);
    }

    returnPoints.Add(transform.position);

    hookProjectileScript.Returned = DeactivateHook;
    hookProjectileScript.Return(returnPoints);
}




    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == HookAbleLayers.GrappleSurface) {
            attachedTransform = collision.transform;
            hookedLayer = HookAbleLayers.GrappleSurface;
        }
        else if (collision.gameObject.layer == HookAbleLayers.PullSurface) {
            hookedLayer = HookAbleLayers.PullSurface;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == HookAbleLayers.GrappleSurface || collision.gameObject.layer == HookAbleLayers.PullSurface)
        {
            hookedLayer = 0;
        }
    }
*/
