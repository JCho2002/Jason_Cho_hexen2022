using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MeteorCard : MoveSet
{
    private Board _board;

    public MeteorCard(Board board) : base(board)
    {
        _board = board;
    }

    public override List<Position> Positions(Position playerPosition, Position hoverPosition)
    {
        List<Position> pos = new List<Position>();

        pos = new MoveSetHelper(hoverPosition, _board)
            .Left(1)
            .UpLeft(1)
            .UpRight(1)
            .Right(1)
            .DownRight(1)
            .DownLeft(1)
            .ValidPositions();

        pos.Add(hoverPosition);

        return pos;
    }

    public override bool Execute(Position playerPosition, Position hoverPosition)
    {
        List<Position> positions = Positions(playerPosition, hoverPosition);

        foreach (Position pos in positions)
        {
            if (!_board.TryGetPieceAt(pos, out var piece))
                continue;

            if (piece.Player == Player.Player)
                continue;

            _board.Take(pos);

        }

        return true;
    }
}
