using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine
{
    private readonly Board _board;
    private readonly MoveSetCollector _moveSetCollection;

    public Engine(Board board)
    {
        _board = board;
        _moveSetCollection = new MoveSetCollector(_board);
    }

    public MoveSetCollector MoveSets
        => _moveSetCollection;

    public bool Move(Position fromPosition, Position toPosition)
    {
        if (!_board.IsValid(fromPosition))
            return false;

        if (!_board.IsValid(toPosition))
            return false;

        if (!_board.TryGetPieceAt(fromPosition, out var piece))
            return false;

        //Fix this Later so that it can track the current card
        if (!MoveSets.TryGetMoveSet(CardType.Teleport, out var moveSet))
            return false;

        if (!moveSet.Positions(fromPosition).Contains(toPosition))
            return false;

        return moveSet.Execute(fromPosition, toPosition);
    }
}
