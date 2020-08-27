using System;

namespace BlackJack
{
    public class Player
    {
        public Card[] cards = new Card[6];
        public bool isDealer = false;
        Card[,] cardsDealt;

        public int wallet;
        
        public Player(Card[] cardsDealt, int wallet)
        {
            cards = cardsDealt;
            this.wallet = wallet;
        }
        
        public Player(Card[] cardsDealt,bool isDealer,Card[,] allCardsDealt, int wallet)
        {
            cards = cardsDealt;
            this.isDealer = isDealer;
            this.cardsDealt = allCardsDealt;
            this.wallet = wallet;
        }

        public bool PlayerPlays()
        {
            bool askCard = false;
            
            int totalPoints;
            int totalPlayerPointsP = 0;
            int totalPlayerPointsWAceP = 0;
            bool wAceP = false;
            
            foreach (Card card in cards)
            {
                if (card != null)
                {
                    
                    if (card.Value == 14)
                    {
                        if (wAceP == false)
                        {
                            wAceP = true;
                            totalPlayerPointsWAceP += totalPlayerPointsP;
                            totalPlayerPointsWAceP += 11;
                            totalPlayerPointsP += 1;
                        }
                        else
                        {
                            totalPlayerPointsWAceP += 11;
                            totalPlayerPointsP += 1;
                        }
                    }
                    else
                    {
                        int cardValue;
                        if (card.Value > 10 && card.Value != 14)
                        {
                            cardValue = 10;
                        }
                        else
                        {
                            cardValue = card.Value;
                        }
                        totalPlayerPointsP += cardValue;
                                    
                        if (wAceP)
                        {
                            totalPlayerPointsWAceP += cardValue;
                        }
                    }
                }
            }
            
            
            
            if (totalPlayerPointsWAceP >= 16)
            {
                totalPoints = totalPlayerPointsWAceP;
            }
            else
            {
                if (totalPlayerPointsWAceP > totalPlayerPointsP)
                {
                    totalPoints = totalPlayerPointsWAceP;
                }
                else
                {
                    totalPoints = totalPlayerPointsP;    
                }
                                
            }
            
            if (isDealer == false)
            {
                // if the player is not the dealer
                
                // if total is less than or equal to 16 ask for another card else stay
                
                if (totalPoints <= 16)
                {
                    askCard = true;
                }
            }
            else
            {
                // if the player is the dealer
                // if current total point is higher than the others
                // if total is less than or equal to 16 ask for another card else stay
                
                int rowLength = cardsDealt.GetLength(0);
                int colLength = cardsDealt.GetLength(1);
                
                int[] playerPoints = new int[rowLength];
                
                for (int i = 0; i < rowLength; i++)
                {
                    int totalPlayerPoints = 0;
                    int totalPlayerPointsWAce = 0;
                    bool wAce = false;
                    
                    if (i != 0)
                    {
                        
                        for (int j = 0; j < colLength; j++)
                        {
                            if (cardsDealt[i,j] != null)
                            {
                                if (cardsDealt[i, j].Value == 14)
                                {
                                    if (wAce == false)
                                    {
                                        wAce = true;
                                        totalPlayerPointsWAce += totalPlayerPoints;
                                        totalPlayerPointsWAce += 11;
                                        totalPlayerPoints += 1;
                                    }
                                    else
                                    {
                                        totalPlayerPointsWAce += 11;
                                        totalPlayerPoints += 1;
                                    }
                                }
                                else
                                {
                                    int cardValue;
                                    if (cardsDealt[i, j].Value > 10 && cardsDealt[i, j].Value != 14)
                                    {
                                        cardValue = 10;
                                    }
                                    else
                                    {
                                        cardValue = cardsDealt[i, j].Value;
                                    }
                                    totalPlayerPoints += cardValue;
                                    
                                    if (wAce)
                                    {
                                        totalPlayerPointsWAce += cardValue;
                                    }
                                }
                            }
                        }
                        if (totalPlayerPointsWAce >= 16)
                        {
                            playerPoints[i] = totalPlayerPointsWAce;
                        }
                        else
                        {
                            if (totalPlayerPointsWAce > totalPlayerPoints)
                            {
                                playerPoints[i] = totalPlayerPointsWAce;
                            }
                            else
                            {
                                playerPoints[i] = totalPlayerPoints;    
                            }
                                
                        }
                    }
                    else
                    {
                        for (int j = 0; j < colLength; j++)
                        {
                            
                            if (cardsDealt[i,j] != null)
                            {
                                if (cardsDealt[i, j].Value == 14)
                                {
                                    if (wAce == false)
                                    {
                                        wAce = true;
                                        totalPlayerPointsWAce += totalPlayerPoints;
                                        totalPlayerPointsWAce += 11;
                                        totalPlayerPoints += 1;
                                    }
                                    else
                                    {
                                        totalPlayerPointsWAce += 11;
                                        totalPlayerPoints += 1;
                                    }
                                }
                                else
                                {
                                    int cardValue;
                                    if (cardsDealt[i, j].Value > 10 && cardsDealt[i, j].Value != 14)
                                    {
                                        cardValue = 10;
                                    }
                                    else
                                    {
                                        cardValue = cardsDealt[i, j].Value;
                                    }
                                    totalPlayerPoints += cardValue;
                                    
                                    if (wAce)
                                    {
                                        totalPlayerPointsWAce += cardValue;
                                    }
                                }
                            }
                        }
                        
                        
                        // if total with ace is higher than 16 choose that else choose the one that is higher
                        if (totalPlayerPointsWAce >= 16)
                        {
                            playerPoints[i] = totalPlayerPointsWAce;
                        }
                        else
                        {
                            if (totalPlayerPointsWAce > totalPlayerPoints)
                            {
                                playerPoints[i] = totalPlayerPointsWAce;
                            }
                            else
                            {
                                playerPoints[i] = totalPlayerPoints;    
                            }
                                
                        }
                        
                    }

                    
                }
                // if current total point is higher than the others
                /*foreach (int points in playerPoints)
                {
                    Console.WriteLine(points);
                }

                foreach (Card card in cardsDealt)
                {
                    if (card != null)
                    {
                        Console.WriteLine(card.Value);
                    }
                }*/

                int dealerPoint = playerPoints[0];
                int comparePoint = 0;

                for (int i = 1; i < playerPoints.Length; i++)
                {
                    if (dealerPoint >= playerPoints[i])
                    {
                        comparePoint++;
                    }
                }
                
                // if compare points is higher than half of rounded table size
                int halfTableSize = Convert.ToInt16((playerPoints.Length - 1) / 2);
                
                if (comparePoint >= halfTableSize)
                {
                    askCard = false;
                }
                else
                {
                    if (dealerPoint < 16)
                    {
                        askCard = true;
                    }
                    
                }



            }
            return askCard;
            
        }

        public int Wallet
        {
            get => wallet;
            set => wallet = value;
        }

        public void PrintMyCards()
        {
            foreach (Card card in cards)
            {
                if (card != null)
                {
                    Console.Write(card.Value);
                    Console.WriteLine(card.Suit);
                }
            }
        }
    }
}