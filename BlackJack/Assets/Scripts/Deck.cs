using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Deck : MonoBehaviour
{
    private List<Cards> deck;

    public Deck(GameObject[] cardPrefabs)
    {
        deck = new List<Cards>();
        foreach (GameObject g in cardPrefabs)
        {
            deck.Add(new Cards(g));
        }
    }
    public Deck(GameObject[] cardPrefabs, int numberOfCardDecks)
    {
        deck = new List<Cards>();
        while (numberOfCardDecks-- > 0)
        {
            foreach (GameObject g in cardPrefabs)
            {
                deck.Add(new Cards(g));
            }
        }
    }

    public Cards DrawRandomCard()
    {
        int index = UnityEngine.Random.Range(0, deck.Count - 1);
        Cards chosen = deck[index];
        deck.RemoveAt(index);
        return chosen;
    }
}
