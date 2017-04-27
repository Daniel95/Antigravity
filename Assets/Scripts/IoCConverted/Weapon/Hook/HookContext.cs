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
        Bind<Ref<IHookProjectile>>();

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
        /*
        private void ShootHook(Vector2 destination, Vector2 spawnPosition) {

            hookProjectileScript.Attached = Hooked;
            hookProjectileScript.Canceled = Canceled;
        }
        */

        On<PullBackHookEvent>()
            .Do<AbortIfHookStatesAreActive>(new List<HookState>() {
                HookState.Inactive,
                HookState.BusyPullingBack })
            .Do<SetHookStateCommand>(HookState.BusyPullingBack)
            .Do<HookProjectileSetAttachedTransformCommand>(null)
            .Do<HookProjectileSetHookedLayerIndexCommand>(0)
            .Do<HookProjectileResetParentCommand>();

        /*
        //pulls the grappling hook back to the player, once it reached the player set it to inactive
        private void PullBack()
        {

            List<Vector2> returnPoints = new List<Vector2>();
            foreach (Transform t in anchors) {
                returnPoints.Add(t.position);
            }

            returnPoints.Add(transform.position);

            hookProjectileScript.Returned = DeactivateHook;
            hookProjectileScript.Return(returnPoints);
        }
        */

        //todo
        On<ReachedDestinationEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfHookedLayerIsZeroCommand>()
            .Do<HookProjectileSetParentToAttachedTransformCommand>()
            .Dispatch<HookIsAttachedEvent>(); //dispatch via command with int parameter

        //make special trigger event when the player touched something!
        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexIsNotTheSameCommand>(HookAbleLayers.GrappleSurface)
            .Do<HookProjectileSetAttachedTransformCommand>()
            .Do<HookProjectileSetHookedLayerIndexCommand>(HookAbleLayers.GrappleSurface);

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexIsNotTheSameCommand>(HookAbleLayers.PullSurface)
            .Do<HookProjectileSetHookedLayerIndexCommand>(HookAbleLayers.PullSurface);

        On<TriggerExit2DEvent>()
            .Do<AbortIfGameObjectIsNotHookProjectileCommand>()
            .Do<AbortIfTriggerLayerIndexesAreNotTheSameCommand>(new List<int> { HookAbleLayers.GrappleSurface, HookAbleLayers.PullSurface })
            .Do<HookProjectileSetHookedLayerIndexCommand>(0);
    }
}



