using System;
using UnityEngine;

public class AmountTileCondition : TileCondition {

    [SerializeField] protected int requiredAmount;
    [SerializeField] protected AmountType amountType;

    public override bool Check(Vector2 gridPosition) {
        throw new NotImplementedException();
    }

    protected bool CheckAmount(int amount) {
        bool hasRequiredAmount = false;

        switch (amountType) {
            case AmountType.IsHigher:
                hasRequiredAmount = amount > requiredAmount;
                break;
            case AmountType.IsLower:
                hasRequiredAmount = amount < requiredAmount;
                break;
            case AmountType.Equals:
                hasRequiredAmount = amount == requiredAmount;
                break;
        }

        return hasRequiredAmount;
    }

    protected void UpdateName(string startName) {
        string conditionName = startName;

        switch (amountType) {
            case AmountType.IsHigher:
                conditionName += " amount is higher then ";
                break;
            case AmountType.IsLower:
                conditionName += " amount is lower then ";
                break;
            case AmountType.Equals:
                conditionName += " amount equals ";
                break;
        }

        conditionName += requiredAmount;

        if (name != conditionName) {
            name = conditionName;
        }
    }

}