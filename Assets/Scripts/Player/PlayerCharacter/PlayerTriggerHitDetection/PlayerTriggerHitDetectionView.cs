using IoCPlus;

public class PlayerTriggerHitDetectionView : TriggerHitDetectionView {

    [Inject(Label.Player)] private Ref<ITriggerHitDetection> playerTriggerHitDetectionRef;

    public override void Initialize() {
        playerTriggerHitDetectionRef.Set(this);
    }
}
