public class TemporarySpeedChangeParameter {

    public float Amount;
    public float NeutralValue = 0.5f;

    public TemporarySpeedChangeParameter(float amount, float neutralValue = 0.5f) {
        Amount = amount;
        NeutralValue = neutralValue;
    }
}
