using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace BlackJack
{
    public class Dealer
    {
        public Card[,] cardsDealt;
        
        public Dealer(int playerNumber)
        {
            cardsDealt = new Card[playerNumber, 6];
            

            //asks if the player wants a card
            //plays his hand 
            //Decides who won or not
        }

        public void DealCards(Deck gameDeck,Card[] garbage,int playerNumber)
        {
            /*
             * Dealing cards for the very first time, 2 for each
             */
            
            
            //Deals hands for every player including the dealer
            int countCard = 0;
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < playerNumber; i++)
                {
                    Card item = new Card(gameDeck.deck[countCard].Value,gameDeck.deck[countCard].Suit);
                    cardsDealt[i, j] = item;
                    countCard++;
                }    
            }
            
            //Dealt hands are popped from the gamedeck and appended to the garbage
            Card[] newGameDeck = new Card[gameDeck.deck.Length - playerNumber*2];
            int count = 0;
            
            for (int i = 0; i < gameDeck.deck.Length; i++)
            {
                if (i <= playerNumber*2)
                {
                    garbage[i] = new Card(gameDeck.deck[i].Value,gameDeck.deck[i].Suit);
                }
                else
                {
                    newGameDeck[count] = new Card(gameDeck.deck[i].Value,gameDeck.deck[i].Suit);
                    count++;
                }
            }
            
            //game deck has new length

            gameDeck.deck = newGameDeck;

        }

        public void DealOnce(Deck gameDeck,Card[] garbage,int playerIndex)
        {
            // how many cards does the player have
            int cardsObtained = 0;
            int rowLength = cardsDealt.GetLength(1);
            for (int i = 0; i < rowLength; i++)
            {
                if (cardsDealt[playerIndex,i] != null)
                {
                    cardsObtained++;
                }    
            }
            
            //append card to the correct place
            cardsDealt[playerIndex,cardsObtained] = new Card(gameDeck.deck[0].Value,gameDeck.deck[0].Suit);
            
            
            
            //garbage
            int garbageLength = 0;
            for (int i = 0; i < garbage.Length; i++)
            {
                if (garbage[i] != null)
                {
                    garbageLength++;
                }
            }
            //new gamedeck
            
            Card[] newGameDeck = new Card[gameDeck.deck.Length - 1];
            int count = 0;
            
            for (int i = 0; i < gameDeck.deck.Length - 1 ; i++)
            {
                if (i == 0)
                {
                    // add it to the correct place 
                    garbage[garbageLength + i] = new Card(gameDeck.deck[i].Value,gameDeck.deck[i].Suit);
                }
                else
                {
                    newGameDeck[count] = new Card(gameDeck.deck[i].Value, gameDeck.deck[i].Suit);
                    count++;
                }
            }
            
            //game deck has new length
            gameDeck.deck = newGameDeck;
            

        }
    }
}