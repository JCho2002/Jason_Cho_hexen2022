using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private LayerMask _UIMask;

    [SerializeField]
    private int _TileMask;

    private Vector3 _startingPos;
    private GameObject _child;
    private CardPositioner _cardPositioner;

    private void Awake()
    {
        _cardPositioner = FindObjectOfType<CardPositioner>();
        _startingPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_child != null)
            _child = null;

        _child = Instantiate(this.gameObject, this.transform.parent);
        Destroy(_child.GetComponent<DragAndDrop>());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.layer == _TileMask)
                _cardPositioner.DestroyCard(transform.parent.gameObject);
        }
        else
        {
            transform.position = _startingPos;
            Destroy(_child);
        }
    }
}
