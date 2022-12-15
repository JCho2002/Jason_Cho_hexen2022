using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPositioner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _cards;

    public void DestroyCard(GameObject card)
    {
        _cards.Remove(card);
        Destroy(card);
    }
}
