using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private LayerMask _UIMask;

    [SerializeField]
    private int _TileMask;

    [SerializeField]
    private CardType _type;

    private Vector3 _startingPos;
    private GameObject _child;
    private CardPositioner _cardPositioner;
    private GameLoop _gameLoop;

    private void Awake()
    {
        _cardPositioner = FindObjectOfType<CardPositioner>();
        _startingPos = transform.position;
        _gameLoop = FindObjectOfType<GameLoop>();
    }

    public CardType Type => _type;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_child != null)
            _child = null;

        _gameLoop.CardSelected(this);

        _child = Instantiate(this.gameObject, this.transform.parent);
        this.GetComponent<Image>().raycastTarget = false;
        Destroy(_child.GetComponent<CardView>());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<Image>().raycastTarget = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit)) {
            if (hit.transform.gameObject.layer == _TileMask && _gameLoop.CardLetGo(hit.transform.gameObject.GetComponent<HexTileView>(), this))
            {
                DestroyClone();
                _cardPositioner.DestroyCard(this.gameObject);
            }
            else
                DestroyClone();
        } else
            DestroyClone();
    }

    private void DestroyClone()
    {
        _gameLoop.ClearCurrentCard();
        transform.position = _startingPos;
        Destroy(_child);
    }
}
