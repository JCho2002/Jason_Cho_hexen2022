using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        var positionViews = GetComponentsInChildren<PositionView>();
        foreach (var positionView in positionViews)
            positionView.Clicked += OnPositionViewClicked;
    }

    private void OnPositionViewClicked(object sender, EventArgs e)
    {
        if (sender is PositionView positionView)
            OnPositionClicked(new PositionEventArgs(positionView.HexGridPosition));
    }

    protected virtual void OnPositionClicked(PositionEventArgs e)
    {
        var handler = PositionClicked;
        handler.Invoke(this, e);
    }
}
