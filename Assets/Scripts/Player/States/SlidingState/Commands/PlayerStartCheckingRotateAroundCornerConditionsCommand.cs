using IoCPlus;
using UnityEngine;

public class PlayerStartCheckingRotateAroundCornerConditionsCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterSliding> playerSlidingRef;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        Vector2 cornerPosition = collider.transform.position;
        Debug.Log("collider " + collider.name, collider.gameObject);
        playerSlidingRef.Get().StartCheckingRotateAroundCornerConditions(cornerPosition);
    }

}
