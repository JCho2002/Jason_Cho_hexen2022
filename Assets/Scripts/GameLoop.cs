using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameLoop : MonoBehaviour
{
    private Board _board;
    private PieceView _player;
    private void Start()
    {
        _board = new Board(PositionHelper.QTileColumn, PositionHelper.RTileColumn);

        _board.PieceMoved += (s, e)
            => e.Piece.MoveTo(PositionHelper.WorldPosition(e.ToPosition));

        _player = FindObjectOfType<PieceView>();

        _board.Place(PositionHelper.GridPosition(_player.WorldPosition), _player);

        var boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;
    }

    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
        _player.MoveTo(PositionHelper.WorldPosition(e.Position));
    }
}
