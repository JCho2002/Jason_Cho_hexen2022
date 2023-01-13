using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class MoveSet : IMoveSet
{
    private Board _board;

    private PieceView _playerView;
    private HexTileView _hoverView;

    protected Board Board => _board;

    public MoveSet(Board board)
    {
        _board = board;
    }

    public abstract List<Position> Positions(Position playerPosition, Position hoverPosition);

    public virtual bool Execute(Position playerPosition, Position hoverPosition)
        => _board.Move(playerPosition, hoverPosition);
}
