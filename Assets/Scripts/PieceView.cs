using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceView : MonoBehaviour
{

    public Vector3 WorldPosition => transform.position;

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
