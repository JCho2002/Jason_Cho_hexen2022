using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal delegate List<Position> Collector(Board board, Position position);

internal class ConfigurableMoveSet : MoveSet
{
    private readonly Collector _collector;

    public ConfigurableMoveSet(Board board, Collector collector) : base(board)
    {
        _collector = collector;
    }

    public override List<Position> Positions(Position fromPosition)
        => _collector(Board, fromPosition);
}
