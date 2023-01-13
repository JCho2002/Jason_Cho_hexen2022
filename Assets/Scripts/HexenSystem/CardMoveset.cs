using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class CardMoveset : IMoveSet
{
    private Board _board;

    public CardMoveset(Board board)
    {
        _board = board;
    }

    public abstract List<Position> Positions(Position playerPosition, Position hoverPosition);


    public virtual bool Execute(Position fromPosition, Position toPosition)
    {
        _board.Take(toPosition);

        return _board.Move(fromPosition, toPosition);
    }
}
