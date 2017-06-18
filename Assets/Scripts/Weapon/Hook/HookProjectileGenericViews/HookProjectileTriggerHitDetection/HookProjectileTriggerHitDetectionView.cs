using IoCPlus;

public class HookProjectileTriggerHitDetectionView : TriggerHitDetectionView {

    [Inject(Label.HookProjectile)] private Ref<ITriggerHitDetection> hookProjectileTriggerHitDetectionRef;

    public override void Initialize() {
        hookProjectileTriggerHitDetectionRef.Set(this);
    }
}
