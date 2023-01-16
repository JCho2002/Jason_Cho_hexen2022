using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/*
public class CardEventArgs : EventArgs
{
    public CardView Card { get; }

    public CardEventArgs(CardView card)
    {
        Card = card;
    }
}

public class PositionCardEventArgs : EventArgs
{
    public Position Position { get; }

    public CardView Card { get; }

    public PositionCardEventArgs(Position position, CardView card)
    {
        Position = position;
        Card = card;
    }
}
*/

public class GameLoop : MonoBehaviour
{
    private StateMachine _stateMachine;

    public StateMachine StateMachine => _stateMachine;

    private void Start()
    {
        _stateMachine = new StateMachine();
        _stateMachine.Register(States.Start, new StartState());
        _stateMachine.Register(States.Playing, new PlayingState());

        _stateMachine.InitialState = States.Start;
    }
}

/*

private Board _board;
private PieceView _player;
private Engine _engine;
private BoardView _boardView;
private CardView _currentCard;

private void Start()
{
    _board = new Board(PositionHelper.HexRadius);

    _board.PieceMoved += (s, e)
        => e.Piece.MoveTo(PositionHelper.WorldPosition(e.ToPosition));

    _board.PieceTaken += (s, e)
        => e.Piece.Taken();

    var pieces = FindObjectsOfType<PieceView>();
    foreach(var piece in pieces)
    {
        _board.Place(PositionHelper.GridPosition(piece.WorldPosition), piece);
        if (piece.Player == Player.Player)
            _player = piece;
    }

    _engine = new Engine(_board);

    _boardView = FindObjectOfType<BoardView>();
    _boardView.PointerEnter += OnPointerEnter;
    _boardView.PointerExit += OnPointerExit;
}

internal void CardSelected(CardView card)
    => OnCardClicked(new CardEventArgs(card));

internal bool CardLetGo(HexTileView tile, CardView card)
=> OnCardDropped(new PositionCardEventArgs(tile.HexGridPosition, card));

internal void CardHoveredOverTile(HexTileView tile)
    => OnPointerEnter(this, new PositionEventArgs(tile.HexGridPosition));

private void OnPointerEnter(object sender, PositionEventArgs e)
{
    if (_currentCard == null)
        return;

    IMoveSet moveSet = _engine.MoveSets.For(_currentCard.Type);
    List<Position> validPositions = moveSet.Positions(PositionHelper.GridPosition(_player.WorldPosition), e.Position);
    _boardView.ActivePosition = validPositions;
}

private void OnPointerExit(object sender, PositionEventArgs e)
{
    _boardView.ActivePosition = new List<Position>(0);
}

private void OnCardClicked(CardEventArgs e)
{
    if (_currentCard != null)
        _currentCard = null;

    _currentCard = e.Card;
}

private bool OnCardDropped(PositionCardEventArgs e)
{
    _boardView.ActivePosition = new List<Position>(0);

    var hoverPosition = e.Position;

    if (!_engine.Action(PositionHelper.GridPosition(_player.WorldPosition), hoverPosition, _currentCard.Type))
    {
        ClearCurrentCard();
        return false;
    }

    ClearCurrentCard();
    return true;

}

internal void ClearCurrentCard()
    => _currentCard = null;
}
*/
