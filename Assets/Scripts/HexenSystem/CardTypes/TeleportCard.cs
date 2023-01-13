using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class TeleportCard : MoveSet
{
    private Board _board;

    public TeleportCard(Board board) : base(board)
    {
        _board = board;
    }

    public override List<Position> Positions(Position playerPosition, Position hoverPosition)
    {
        if (_board.TryGetPieceAt(hoverPosition, out var piece))
            return new List<Position>(0);

        List<Position> pos = new List<Position>();
        pos.Add(hoverPosition);

        return pos;

    }

    public override bool Execute(Position playerPosition, Position hoverPosition)
    {
        return _board.Move(playerPosition, hoverPosition);
    }
}
