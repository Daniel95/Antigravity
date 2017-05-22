using IoCPlus;

public class CharacterTemporarySpeedChangeEvent : Signal<CharacterTemporarySpeedChangeEvent.Parameter> {

    public class Parameter {
        public float Amount;
        public float NeutralValue = 0.5f;

        public Parameter(float amount, float neutralValue = 0.5f) {
            Amount = amount;
            NeutralValue = neutralValue;
        }
    }
}
