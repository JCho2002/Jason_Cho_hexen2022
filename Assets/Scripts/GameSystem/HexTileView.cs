using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[Serializable]
public class ActivationChangedUnityEvent : UnityEvent<bool> { }

public class HexTileView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private UnityEvent OnActivate;

    [SerializeField]
    private UnityEvent OnDeactivate;

    [SerializeField]
    private ActivationChangedUnityEvent OnActivationChanged;

    public BoardView _parent;
    
    public Position HexGridPosition => PositionHelper.GridPosition(transform.position);

    private void Start()
   => _parent = GetComponentInParent<BoardView>();

    public void OnPointerEnter(PointerEventData eventData)
        => _parent.ChildEntered(this);

    public void OnPointerExit(PointerEventData eventData)
        => _parent.ChildExited(this);

    internal void Activate()
    {
        OnActivate?.Invoke();
        OnActivationChanged?.Invoke(true);
    }

    internal void Deactivate()
    {
        OnDeactivate?.Invoke();
        OnActivationChanged?.Invoke(false);
    }
}
