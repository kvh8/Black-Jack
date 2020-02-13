using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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



    //public void OnClick()             For Buttons
    //{
    //    for (var i = 0; i < 2; i++)
    //    {
    //        GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
    //        playerCard.transform.SetParent(PlayerArea.transform, false);
    //        GameObject opponentCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
    //        opponentCard.transform.SetParent(OpponentArea.transform, false);
    //    }
    //}













    // Start is called before the first frame update
    void Start()
    {
        //playerMoney = 1000;
        //currentBet = 50;
        //resetGame();

        //primaryBtn.onClick.AddListener(delegate {
        //    if (isPlaying)
        //    {
        //        playerDrawCard();
        //    }
        //    else
        //    {
        //        startGame();
        //    }
        //});

        //secondaryBtn.onClick.AddListener(delegate {
        //    playerEndTurn();
        //});

        //betSlider.onValueChanged.AddListener(delegate {
        //    updateCurrentBet();
        //});

        //resetBalanceBtn.onClick.AddListener(delegate {
        //    playerMoney = 1000;
        //    betSlider.maxValue = playerMoney;
        //});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
