using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class MoveSetHelper
{
    private PieceView _currentPiece;
    private readonly Position _currentHexPosition;
    private Board _board;
    private List<Position> _positions = new List<Position>();

    public MoveSetHelper(Position position, Board board)
    {
        _currentHexPosition = position;
        _board = board;

        if (!_board.TryGetPieceAt(_currentHexPosition, out _currentPiece))
            Debug.Log($"Passed in a position {_currentHexPosition} which contains no piece to {nameof(MoveSetHelper)}.");
    }

    public MoveSetHelper UpLeft(int maxSteps = int.MaxValue, params Validator[] condition)
                => Collect(new Vector2Int(0, -1), maxSteps, condition);

    public MoveSetHelper UpRight(int maxSteps = int.MaxValue, params Validator[] condition)
        => Collect(new Vector2Int(1, -1), maxSteps, condition);

    public MoveSetHelper Right(int maxSteps = int.MaxValue, params Validator[] condition)
        => Collect(new Vector2Int(1, 0), maxSteps, condition);

    public MoveSetHelper DownRight(int maxSteps = int.MaxValue, params Validator[] condition)
        => Collect(new Vector2Int(0, 1), maxSteps, condition);

    public MoveSetHelper DownLeft(int maxSteps = int.MaxValue, params Validator[] condition)
        => Collect(new Vector2Int(-1, 1), maxSteps, condition);

    public MoveSetHelper Left(int maxSteps = int.MaxValue, params Validator[] condition)
        => Collect(new Vector2Int(-1, 0), maxSteps, condition);

    public delegate bool Validator(Position currentPosition, Board board, Position targetTile);

    public MoveSetHelper Collect(Vector2Int direction, int maxSteps = int.MaxValue, params Validator[] condition)
    {
        if (_currentPiece == null)
            return this;

        var currentStep = 0;
        var position = new Position(_currentHexPosition.Q + direction.x, _currentHexPosition.R + direction.y);

        while (_board.IsValid(position)
            && currentStep < maxSteps
            && (condition == null || condition.All((v) => v(_currentHexPosition, _board, position)))
            )
        {
            if (_board.TryGetPieceAt(position, out var piece))
            {
                if (piece.Player != _currentPiece.Player)
                    _positions.Add(position);

                break;
            }

            _positions.Add(position);

            position = new Position(position.Q + direction.x, position.R + direction.y);
            currentStep++;
        }

        return this;
    }

    public List<Position> ValidPositions()
    {
        return _positions;
    }
}
