using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cardPrefabs, playerCardPosition, dealerCardPosition, deckCardPosition;
    [SerializeField]
    private GameObject backCardPrefab;
    [SerializeField]
    private Button hitDeal, stand, resetBalance, betFiveHundred, betOneHundred, betFifty;
    [SerializeField]
    private Text textMoney, textBet, textPlayerPoints, textDealerPoints, textPlaceYourBet, textSelectingBet, textWinner;
    [SerializeField]
    private Image resetImgBtn;


    










    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
