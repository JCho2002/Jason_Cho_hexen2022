using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class LineCard : MoveSet
{
    private Board _board;

    public LineCard(Board board) : base(board)
    {
        _board = board;
    }

    public override List<Position> Positions(Position playerPosition, Position hoverPosition)
    {
        List<Position> combinedTilesList = new MoveSetHelper(playerPosition, _board)
            .Left()
            .UpLeft()
            .UpRight()
            .Right()
            .DownRight()
            .DownLeft()
            .ValidPositions();

        if (!combinedTilesList.Contains(hoverPosition))
            return combinedTilesList;

        int Q = Math.Sign(hoverPosition.Q - playerPosition.Q);
        int R = Math.Sign(hoverPosition.R - playerPosition.R);

        Vector2Int normalizedDir = new Vector2Int(Q, R);

        List<Position> directedLine = new MoveSetHelper(playerPosition, _board).Collect(normalizedDir).ValidPositions();

        return directedLine;

    }

    public override bool Execute(Position playerPosition, Position hoverPosition)
    {
        List<Position> positions = Positions(playerPosition, hoverPosition);

        foreach (Position pos in positions)
            _board.Take(pos);

        return true;
    }

}
