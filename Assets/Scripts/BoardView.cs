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
    public event EventHandler<PositionEventArgs> PositionClicked;

    internal void ChildClicked(PositionView positionView)
    => OnPositionClicked(new PositionEventArgs(positionView.HexGridPosition));

    protected virtual void OnPositionClicked(PositionEventArgs e)
    {
        var handler = PositionClicked;
        handler.Invoke(this, e);
    }
}
