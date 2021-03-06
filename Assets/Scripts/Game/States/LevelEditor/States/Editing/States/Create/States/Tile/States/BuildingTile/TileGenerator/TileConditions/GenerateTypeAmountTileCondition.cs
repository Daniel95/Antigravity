﻿using System.Collections.Generic;
using UnityEngine;

public class GenerateTypeAmountTileCondition : AmountTileCondition {

    [SerializeField] private GenerateType generateType = GenerateType.All;

    protected bool CheckGenerateTypeAmount(List<Vector2> gridPositions) {
        int amount = 0;

        switch (generateType) {
            case GenerateType.All:
                amount = gridPositions.Count;
                break;
            case GenerateType.UserGenerated:
                List<Vector2> userGeneratedGridPositions = gridPositions.FindAll(x => TileGrid.Instance.GetTile(x).UserGenerated);
                amount = userGeneratedGridPositions.Count;
                break;
            case GenerateType.AutoGenerated:
                List<Vector2> autoGeneratedGridPositions = gridPositions.FindAll(x => !TileGrid.Instance.GetTile(x).UserGenerated);
                amount = autoGeneratedGridPositions.Count;
                break;
        }

        return CheckAmount(amount);
    }

    protected override void UpdateName(string startName) {
        string generateTypeName = generateType.ToString();

        base.UpdateName(generateTypeName + " " + startName);
    }

}
