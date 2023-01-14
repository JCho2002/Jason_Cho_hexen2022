using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPositioner : MonoBehaviour
{
    public int baseDeckSize = 5;
    public List<GameObject> baseCards;
    public Transform firstPos;
    public Transform lastPos;
    public Transform centerPos;

    private void Awake()
    {
        for (int i = 0; i < baseCards.Count; i++)
        {
            var temp = baseCards[i];
            int randomIndex = Random.Range(i, baseCards.Count);
            baseCards[i] = baseCards[randomIndex];
            baseCards[randomIndex] = temp;
        }
        DisplayCard(baseDeckSize);
    }

    private void DisplayCard(int length)
    {
        float space = (lastPos.position.x - firstPos.position.x) / (baseDeckSize - 1);
        if (baseCards.Count >= baseDeckSize)
        {
            for (int i = 0; i < length; i++)
            {
                baseCards[i].transform.position = new Vector2(firstPos.position.x + space * i, firstPos.position.y);
                baseCards[i].SetActive(true);
            }
        } else
        {
            switch (length)
            {
                case 1:
                    baseCards[0].transform.position = new Vector2(centerPos.position.x, centerPos.position.y);
                    break;
                case 2:
                    baseCards[0].transform.position = new Vector2(centerPos.position.x - space / 2, centerPos.position.y);
                    baseCards[1].transform.position = new Vector2(centerPos.position.x + space / 2, centerPos.position.y);
                    break;
                case 3:
                    baseCards[0].transform.position = new Vector2(centerPos.position.x - space, centerPos.position.y);
                    baseCards[1].transform.position = new Vector2(centerPos.position.x, centerPos.position.y);
                    baseCards[2].transform.position = new Vector2(centerPos.position.x + space, centerPos.position.y);
                    break;
                case 4:
                    baseCards[0].transform.position = new Vector2(centerPos.position.x - (3 * space) /2, centerPos.position.y);
                    baseCards[1].transform.position = new Vector2(centerPos.position.x - space / 2, centerPos.position.y);
                    baseCards[2].transform.position = new Vector2(centerPos.position.x + space/ 2, centerPos.position.y);
                    baseCards[3].transform.position = new Vector2(centerPos.position.x + (3 * space) / 2, centerPos.position.y);
                    break;


            }
        }
    }

    public void DestroyCard(GameObject card)
    {
        baseCards.Remove(card);
        Destroy(card);

        if (baseCards.Count >= baseDeckSize)
            DisplayCard(baseDeckSize);
        else
        {
            DisplayCard(baseCards.Count);

            foreach (var c in baseCards)
                c.GetComponent<CardView>().StartingPos = c.transform.position;
        }
    }
}
