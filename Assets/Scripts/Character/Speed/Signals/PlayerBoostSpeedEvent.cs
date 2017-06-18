using IoCPlus;

public class PlayerBoostSpeedEvent : Signal<PlayerBoostSpeedEvent.Parameter> {

    public class Parameter {

        public float NewSpeed;
        public float ReturnSpeed;

        public Parameter(float newSpeed, float returnSpeed) {
            NewSpeed = newSpeed;
            ReturnSpeed = returnSpeed;
        }
    }
}
