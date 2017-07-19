using IoCPlus;
using UnityEngine;

public class PlayerTurnDirectionView : CharacterTurnDirectionView {

    [Inject(Label.Player)] private Ref<ICharacterTurnDirection> playerTurnDirectionRef;

    [Inject] private PlayerTemporarySpeedChangeEvent characterTemporarySpeedChangeEvent;
    [Inject] private PlayerTemporarySpeedDecreaseEvent characterTemporarySpeedDecreaseEvent;

    public override void Initialize() {
        playerTurnDirectionRef.Set(this);
    }

    private void OnEnable() {
        OnSurfaceTurn += SurfaceTurn;
        OnCornerTurn += CornerTurn;
    }

    private void OnDisable() {
        OnSurfaceTurn -= SurfaceTurn;
        OnCornerTurn -= CornerTurn;
    }

    private void SurfaceTurn(Vector2 moveDirection, Vector2 surroundingDirection) {
        float speedChange = Vector2.Angle(moveDirection, surroundingDirection) / 90;

        if (speedChange > MaxSpeedChange) {
            speedChange = MaxSpeedChange;
        }

        characterTemporarySpeedChangeEvent.Dispatch(new PlayerTemporarySpeedChangeEvent.Parameter(speedChange, DirectionSpeedNeutralValue));
    }

    private void CornerTurn(Vector2 moveDirection, Vector2 surroundingDirection) {
        characterTemporarySpeedDecreaseEvent.Dispatch();
    }
}
