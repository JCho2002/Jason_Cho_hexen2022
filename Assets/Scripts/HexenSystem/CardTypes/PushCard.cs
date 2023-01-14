using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class PushCard : MoveSet
{
    private Board _board;
    private List<Position> _playerValidPositions;
    private List<Position> _hoverValidPositions;

    public PushCard(Board board) : base(board)
    {
        _board = board;
    }

    public override List<Position> Positions(Position playerPosition, Position hoverPosition)
    {
        List<Position> _intersection = new List<Position>();

        _playerValidPositions = new MoveSetHelper(playerPosition, _board)
            .Left(1)
            .UpLeft(1)
            .UpRight(1)
            .Right(1)
            .DownRight(1)
            .DownLeft(1)
            .ValidPositions();

        if (!_playerValidPositions.Contains(hoverPosition))
            return _playerValidPositions;

        _hoverValidPositions = new MoveSetHelper(hoverPosition, _board)
            .Left(1)
            .UpLeft(1)
            .UpRight(1)
            .Right(1)
            .DownRight(1)
            .DownLeft(1)
            .ValidPositions();

        IEnumerable<Position> both = _playerValidPositions.Intersect(_hoverValidPositions);

        foreach (Position pos in both)
            _intersection.Add(pos);

        _intersection.Add(hoverPosition);

        return _intersection;
    }

    public override bool Execute(Position playerPosition, Position hoverPosition)
    {
        List<Position> positions = Positions(playerPosition, hoverPosition);

        foreach (Position pos in positions)
        {
            if (!_board.TryGetPieceAt(pos, out var piece))
                continue;

            int Q = Math.Sign(pos.Q - playerPosition.Q);
            int R = Math.Sign(pos.R - playerPosition.R);
            Vector2Int normalizedDir = new Vector2Int(Q, R);
            List<Position> directedLine = new MoveSetHelper(playerPosition, _board).Collect(normalizedDir).ValidPositions();

            if (directedLine.Count <= 1)
            {
                _board.Take(pos);
                continue;
            }

            if (_board.TryGetPieceAt(directedLine[1], out var piece2))
                continue;
            
            _board.Move(pos, directedLine[1]);
            
        }

        return true;
    }
}
