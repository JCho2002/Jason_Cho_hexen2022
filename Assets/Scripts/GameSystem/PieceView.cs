using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceView : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    public Vector3 WorldPosition => transform.position;

    public Player Player => _player;

    internal void MoveTo(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }

    internal void Placed(Vector3 worldPosition)
    {
        transform.position = worldPosition;
        gameObject.SetActive(true);
    }
}
