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

    private List<Cards> playerCards;
    private List<Cards> dealerCards;
    private bool isPlaying;
    private int playerPoints;
    private int actualDealerPoints, displayDealerPoints;
    private int playerMoney;
    private int currentBet;
    private int playerCardPointer, dealerCardPointer;
    private Deck playingDeck;

    // Start is called before the first frame update
    void Start()            
    {
        playerMoney = 1000;
        currentBet = 50;
        resetGame();

        hitDeal.onClick.AddListener(delegate
        {
            if (isPlaying)
            {
                playerDrawCard();
            }
            else
            {
                startGame();
            }
        });

        stand.onClick.AddListener(delegate
        {
            playerEndTurn();
        });

        updateCurrentBet();

        resetBalance.onClick.AddListener(delegate
        {
            playerMoney = 1000;
        });
    }

    void Update()
    {
        textMoney.text = "$" + playerMoney.ToString();
    }
    public void startGame()     
    {
        if (playerMoney > 50)
        {
            playerMoney -= currentBet;
            if (playerMoney < 0)
            {
                playerMoney += currentBet;

                if(playerMoney < 500)
                {
                    betFiveHundred.gameObject.SetActive(false);
                }
                else if(playerMoney < 100)
                {
                    betOneHundred.gameObject.SetActive(false);
                }
                else if(playerMoney < 50)
                {
                    endGame();
                }

                return;
            }

            isPlaying = true;

            // Update UI accordingly

           
            textSelectingBet.gameObject.SetActive(false);
            textPlaceYourBet.gameObject.SetActive(false);
            hitDeal.GetComponentInChildren<Text>().text = "HIT";
            stand.gameObject.SetActive(true);
            textBet.text = "Bet: $" + currentBet.ToString();
            resetBalance.gameObject.SetActive(false);

            // assign the playing deck with 2 deck of cards
            playingDeck = new Deck(cardPrefabs, 2);

            // draw 1 card for player then 1 for dealer
            playerDrawCard();
            dealerDrawCard();
            // draw 1 card for player then 1 for dealer
            playerDrawCard();
            dealerDrawCard();
            updatePlayerPoints();
            updateDealerPoints(true);

            checkIfPlayerBlackjack();
        }
    }

    private void checkIfPlayerBlackjack()
    {
        if (playerPoints == 21)
        {
            playerBlackjack();
        }
    }

    public void endGame()       
    {
        hitDeal.gameObject.SetActive(false);
        stand.gameObject.SetActive(false);
        betFiveHundred.gameObject.SetActive(false);
        betOneHundred.gameObject.SetActive(false);
        betFifty.gameObject.SetActive(false);
        textPlaceYourBet.text = "";
        textSelectingBet.text = "";

        resetImgBtn.gameObject.SetActive(true);
        resetImgBtn.GetComponent<Button>().onClick.AddListener(delegate {
            resetGame();
        });
    }

    public void dealerDrawCard()
    {
        Cards drawnCard = playingDeck.DrawRandomCard();
        GameObject prefab;
        dealerCards.Add(drawnCard);
        if (dealerCardPointer <= 0)
        {
            prefab = backCardPrefab;
        }
        else
        {
            prefab = drawnCard.Prefab;
        }
        Instantiate(prefab, dealerCardPosition[dealerCardPointer++].transform);
        updateDealerPoints(false);
    }

    public void playerDrawCard()
    {
        Cards drawnCard = playingDeck.DrawRandomCard();
        playerCards.Add(drawnCard);
        Instantiate(drawnCard.Prefab, playerCardPosition[playerCardPointer++].transform);
        updatePlayerPoints();
        if (playerPoints > 21)
            playerBusted();
    }

    private void playerEndTurn()
    {
        revealDealersDownFacingCard();
        // dealer start drawing
        while (actualDealerPoints < 17 && actualDealerPoints < playerPoints)
        {
            dealerDrawCard();
        }
        updateDealerPoints(false);
        if (actualDealerPoints > 21)
            dealerBusted();
        else if (actualDealerPoints > playerPoints)
            dealerWin(false);
        else if (actualDealerPoints == playerPoints)
            gameDraw();
        else
            playerWin(false);
    }

    private void revealDealersDownFacingCard()
    {
        // reveal the dealer's down-facing card
        Destroy(dealerCardPosition[0].transform.GetChild(0).gameObject);
        Instantiate(dealerCards[0].Prefab, dealerCardPosition[0].transform);
    }

    private void updatePlayerPoints()
    {
        playerPoints = 0;
        foreach (Cards c in playerCards)
        {
            playerPoints += c.Point;
        }

        // transform ace to 1 if there is any
        if (playerPoints > 21)
        {
            playerPoints = 0;
            foreach (Cards c in playerCards)
            {
                if (c.Point == 11)
                    playerPoints += 1;
                else
                    playerPoints += c.Point;
            }
        }

        textPlayerPoints.text = playerPoints.ToString();
    }

    private void updateDealerPoints(bool hideFirstCard)
    {
        actualDealerPoints = 0;
        foreach (Cards c in dealerCards)
        {
            actualDealerPoints += c.Point;
        }

        // transform ace to 1 if there is any
        if (actualDealerPoints > 21)
        {
            actualDealerPoints = 0;
            foreach (Cards c in dealerCards)
            {
                if (c.Point == 11)
                    actualDealerPoints += 1;
                else
                    actualDealerPoints += c.Point;
            }
        }

        if (hideFirstCard)
            displayDealerPoints = dealerCards[1].Point;
        else
            displayDealerPoints = actualDealerPoints;
        textDealerPoints.text = displayDealerPoints.ToString();
    }

    private void updateCurrentBet()          
    {
        betFiveHundred.onClick.AddListener(delegate
        {
            currentBet += 500;
            textSelectingBet.text = "$" + currentBet.ToString();
        });

        betOneHundred.onClick.AddListener(delegate
        {
            currentBet += 100;
            textSelectingBet.text = "$" + currentBet.ToString();
        });

        betFifty.onClick.AddListener(delegate
        {
            currentBet += 50;
            textSelectingBet.text = "$" + currentBet.ToString();
        });
    }

    private void playerBusted()
    {
        dealerWin(true);
    }

    private void dealerBusted()
    {
        playerWin(true);
    }

    private void playerBlackjack()
    {
        textWinner.text = "Blackjack!";
        playerMoney += currentBet * 2;
        endGame();
    }

    private void playerWin(bool winByBust)
    {
        if (winByBust)
            textWinner.text = "Dealer Busted\nPlayer Wins!";
        else
            textWinner.text = "Player Wins!";
        playerMoney += currentBet * 2;
        endGame();
    }
 

    private void dealerWin(bool winByBust)
    {
        if (winByBust)
            textWinner.text = "Player Busted\nDealer Wins";
        else
            textWinner.text = "Dealer Wins";
        endGame();
    }

    private void gameDraw()
    {
        textWinner.text = "Draw";
        playerMoney += currentBet;
        endGame();
    }

    private void resetGame()        
    {
        isPlaying = false;

        // reset points
        playerPoints = 0;
        actualDealerPoints = 0;
        playerCardPointer = 0;
        dealerCardPointer = 0;

        // reset cards
        playingDeck = new Deck(cardPrefabs, 2);
        playerCards = new List<Cards>();
        dealerCards = new List<Cards>();

        // reset UI
        hitDeal.gameObject.SetActive(true);
        hitDeal.GetComponentInChildren<Text>().text = "DEAL";
        stand.gameObject.SetActive(false);
        betFiveHundred.gameObject.SetActive(true);
        betOneHundred.gameObject.SetActive(true);
        betFifty.gameObject.SetActive(true);
        textSelectingBet.gameObject.SetActive(true);
        textSelectingBet.text = "$" + currentBet.ToString();
        textPlaceYourBet.gameObject.SetActive(true);
        textPlayerPoints.text = "";
        textDealerPoints.text = "";
        textBet.text = "";
        textWinner.text = "";
        resetImgBtn.gameObject.SetActive(false);
        resetBalance.gameObject.SetActive(true);

        // clear cards on table
        clearCards();
    }

    private void clearCards()
    {
        foreach (GameObject g in playerCardPosition)
        {
            if (g.transform.childCount > 0)
                for (int i = 0; i < g.transform.childCount; i++)
                {
                    Destroy(g.transform.GetChild(i).gameObject);
                }
        }
        foreach (GameObject g in dealerCardPosition)
        {
            if (g.transform.childCount > 0)
                for (int i = 0; i < g.transform.childCount; i++)
                {
                    Destroy(g.transform.GetChild(i).gameObject);
                }
        }
    }



}

//Still need to activate the betting buttons
//Also figure out how to make multiple playing decks an option
//A plus is figuring out an aniamtion from the deck in the center