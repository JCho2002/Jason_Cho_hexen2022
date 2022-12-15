using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CardEventArgs : EventArgs
{
    public Card Card { get; }

    public CardEventArgs(Card card)
    {
        Card = card;
    }
}

public class PositionCardEventArgs : EventArgs
{
    public Position Position { get; }

    public Card Card { get; }

    public PositionCardEventArgs(Position position, Card card)
    {
        Position = position;
        Card = card;
    }
}

public class GameLoop : MonoBehaviour
{
    public event EventHandler<CardEventArgs> CardClicked;
    public event EventHandler<PositionCardEventArgs> CardDropped;

    private Board _board;
    private PieceView _player;
    private Engine _engine;
    private BoardView _boardView;
    private Card _currentCard;

    private void Start()
    {
        _board = new Board(PositionHelper.HexRadius);

        _board.PieceMoved += (s, e)
            => e.Piece.MoveTo(PositionHelper.WorldPosition(e.ToPosition));

        _player = FindObjectOfType<PieceView>();

        _board.Place(PositionHelper.GridPosition(_player.WorldPosition), _player);

        _engine = new Engine(_board);

        _boardView = FindObjectOfType<BoardView>();
        _boardView.PointerEnter += OnPointerEnter;
        _boardView.PointerExit += OnPointerExit;
    }

    internal void CardSelected(Card card)
        => OnCardClicked(new CardEventArgs(card));

    internal void CardLetGo(HexTileView tile, Card card)
    => OnCardDropped(new PositionCardEventArgs(tile.HexGridPosition ,card));

    private void OnPointerEnter(object sender, PositionEventArgs e)
    {
        if (_currentCard == null)
            return;
    }

    private void OnPointerExit(object sender, PositionEventArgs e)
    {

    }

    private void OnCardClicked(CardEventArgs e)
    {
        if (_currentCard != null)
            _currentCard = null;

        _currentCard = e.Card;

        IMoveSet moveSet = _engine.MoveSets.For(_currentCard.Type);
        List<Position> validPositions = moveSet.Positions(PositionHelper.GridPosition(_player.WorldPosition));

        _boardView.ActivePosition = validPositions;
    }

    private void OnCardDropped(PositionCardEventArgs e)
    {
        Debug.Log("Card Dropped");
        var toPosition = e.Position;

        _engine.Move(PositionHelper.GridPosition(_player.WorldPosition), toPosition);
    }
}
