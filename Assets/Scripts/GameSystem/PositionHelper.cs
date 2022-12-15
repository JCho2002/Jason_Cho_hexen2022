using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PositionHelper
{
    public const float DiagonalTileSize = 1.75f;
    public const float VerticalTileSize = 3f;
    public const int HexRadius = 3;

    public static Position GridPosition(Vector3 worldPosition)
    {
        Vector3 scaledWorldPosition = new Vector3(worldPosition.x / DiagonalTileSize, worldPosition.y, - worldPosition.z / VerticalTileSize);

        var gridPositionR = (int) scaledWorldPosition.z;
        var gridPositionQ = (int) ((scaledWorldPosition.x - gridPositionR) / 2);

        return new Position(gridPositionQ, gridPositionR);
    }

    public static Vector3 WorldPosition(Position gridPosition)
    {
        var scaledWorldPositionZ = gridPosition.R;
        var scaledWorldPositionX = 2 * gridPosition.Q + gridPosition.R;

        return new Vector3(scaledWorldPositionX * DiagonalTileSize, 0, - scaledWorldPositionZ * VerticalTileSize);


    }
}
