using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{

    public GameObject Card1;
    public GameObject Card2;
    public GameObject PlayerArea;
    public GameObject DealerArea;

    List<GameObject> deck = new List<GameObject>();


    void Start()
    {
        deck.Add(Card1);
        deck.Add(Card2);
    }

    public void OnClick()
    {
        //for (var i = 0; i < 1; i++)
        //{
        //    GameObject playerCard = Instantiate(cards[Random.Range(0, deck.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        //    playerCard.transform.SetParent(PlayerArea.transform, false);
        //    GameObject opponentCard = Instantiate(cards[Random.Range(0, deck.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        //    dealerCard.transform.SetParent(DealerArea.transform, false);
        //}



    }



}
