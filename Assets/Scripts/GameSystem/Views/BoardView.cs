using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PositionEventArgs : EventArgs
{
    public Position Position { get; }

    public PositionEventArgs(Position position)
    {
        Position = position;
    }
}

public class BoardView : MonoBehaviour
{
    public event EventHandler<PositionEventArgs> PointerEnter;
    public event EventHandler<PositionEventArgs> PointerExit;

    private Dictionary<Position, HexTileView> _hexTileViews = new Dictionary<Position, HexTileView>();

    private List<Position> _activePosition = new List<Position>();

    public Dictionary<Position, HexTileView> HexTileViews => _hexTileViews;

    public List<Position> ActivePosition
    {
        set
        {
            foreach (var position in _activePosition)
            {
                _hexTileViews[position].Deactivate();
            }

            if (value == null)
                _activePosition.Clear();
            else
                _activePosition = value;

            foreach (var position in _activePosition)
                _hexTileViews[position].Activate();
        }
    }

    private void OnEnable()
    {
        var hexViews = GetComponentsInChildren<HexTileView>();
        foreach (var hexView in hexViews)
        {
            _hexTileViews.Add(hexView.HexGridPosition, hexView);
        }
    }

    internal void ChildEntered(HexTileView positionView)
        => OnPointerEnter(new PositionEventArgs(positionView.HexGridPosition));

    internal void ChildExited(HexTileView positionView)
    => OnPointerExit(new PositionEventArgs(positionView.HexGridPosition));

    protected virtual void OnPointerEnter(PositionEventArgs e)
    {
        var handler = PointerEnter;
        handler?.Invoke(this, e);
    }

    protected virtual void OnPointerExit(PositionEventArgs e)
    {
        var handler = PointerExit;
        handler?.Invoke(this, e);
    }
}
