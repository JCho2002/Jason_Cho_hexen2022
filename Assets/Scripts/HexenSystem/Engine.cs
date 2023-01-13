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

    public bool Action(Position playerPosition, Position hoverPosition, CardType card)
    {
        if (!_board.IsValid(playerPosition))
            return false;

        if (!_board.IsValid(hoverPosition))
            return false;

        if (playerPosition.Equals(hoverPosition))
            return false;

        if (!MoveSets.TryGetMoveSet(card, out var moveSet))
            return false;

        if (!moveSet.Positions(playerPosition, hoverPosition).Contains(hoverPosition))
            return false;

        return moveSet.Execute(playerPosition, hoverPosition);
    }
}
