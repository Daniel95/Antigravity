public class CharacterTemporarySpeedChangeParameter {

    public float Amount;
    public float NeutralValue = 0.5f;

    public CharacterTemporarySpeedChangeParameter(float amount, float neutralValue = 0.5f) {
        Amount = amount;
        NeutralValue = neutralValue;
    }
}
