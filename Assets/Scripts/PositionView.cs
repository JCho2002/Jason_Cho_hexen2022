using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionView : MonoBehaviour, IPointerClickHandler
{
    public BoardView _parent;

    public Position HexGridPosition => PositionHelper.GridPosition(transform.position);

    private void Start()
   => _parent = GetComponentInParent<BoardView>();


    public void OnPointerClick(PointerEventData eventData)
        => _parent.ChildClicked(this);
}
