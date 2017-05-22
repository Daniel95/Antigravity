using IoCPlus;

public class CharacterBoostSpeedEvent : Signal<CharacterBoostSpeedEvent.Parameter> {

    public class Parameter {

        public float NewSpeed;
        public float ReturnSpeed;

        public Parameter(float newSpeed, float returnSpeed) {
            NewSpeed = newSpeed;
            ReturnSpeed = returnSpeed;
        }
    }
}
