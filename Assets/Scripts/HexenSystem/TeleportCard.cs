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

    public override List<Position> Positions(Position fromPosition)
    {
        var hexTileDictionary = _board.BoardView.HexTileViews;

        return new List<Position>(hexTileDictionary.Keys);

    }

    public override bool Execute(Position fromPosition, Position toPosition)
    {
        return _board.Move(fromPosition, toPosition);
    }
}
