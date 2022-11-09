using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameLoop : MonoBehaviour
{
    private void Start()
    {
        var boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;
    }

    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
        Debug.Log(e.Position.ToString());
    }
}
