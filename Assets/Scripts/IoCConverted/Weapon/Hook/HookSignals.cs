using IoCPlus;
using UnityEngine;

public class CancelHookEvent : Signal { }
public class ChangeSpeedByAngleEvent : Signal { }
public class AddAnchorEvent : Signal<Vector2, Transform> {  }