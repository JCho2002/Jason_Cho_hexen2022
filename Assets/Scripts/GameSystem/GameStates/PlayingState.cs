using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

public class PlayingState : State
{
    private Board _board;
    private PieceView _player;
    private Engine _engine;
    private BoardView _boardView;
    private CardView _currentCard;

    private void InitializeScene(AsyncOperation obj)
    {
        _boardView = GameObject.FindObjectOfType<BoardView>();
        if (_boardView != null)
        {
            _boardView.PointerEnter += OnPointerEnter;
            _boardView.PointerExit += OnPointerExit;
        }

        var pieceViews = GameObject.FindObjectsOfType<PieceView>();
        foreach (var pieceView in pieceViews)
        {
            _board.Place(PositionHelper.GridPosition(pieceView.WorldPosition), pieceView);
            if (pieceView.Player == Player.Player)
                _player = pieceView;
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();

        _board = new Board(PositionHelper.HexRadius);

        _board.PieceMoved += (s, e)
            => e.Piece.MoveTo(PositionHelper.WorldPosition(e.ToPosition));

        _board.PieceTaken += (s, e)
            => e.Piece.Taken();

        _engine = new Engine(_board);

        var op = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        op.completed += InitializeScene;
    }

    public override void OnExit()
    {
        base.OnExit();
        if (_boardView != null)
        {
            _boardView.PointerEnter -= OnPointerEnter;
            _boardView.PointerExit -= OnPointerExit;
        }

        SceneManager.UnloadSceneAsync("Game");
    }

    public override void OnSuspend()
    {
        if (_boardView != null)
        {
            _boardView.PointerEnter -= OnPointerEnter;
            _boardView.PointerExit -= OnPointerExit;
        }
    }

    public override void OnResume()
    {
        if (_boardView != null)
        {
            _boardView.PointerEnter += OnPointerEnter;
            _boardView.PointerExit += OnPointerExit;
        }
    }

    internal void CardSelected(CardView card)
    => OnCardClicked(new CardEventArgs(card));

    internal bool CardLetGo(HexTileView tile, CardView card)
    => OnCardDropped(new PositionCardEventArgs(tile.HexGridPosition, card));

    /*internal void CardHoveredOverTile(HexTileView tile)
        => OnPointerEnter(this, new PositionEventArgs(tile.HexGridPosition));
    */

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
