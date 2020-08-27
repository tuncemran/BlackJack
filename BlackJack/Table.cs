using System;

namespace BlackJack
{
    public class Table
    {
        int PlayerNumber;
        private int defaultWallet = 1000;

        public Table(int playerNumber, int numberOfDecks)
        {
            // Number of players 
            PlayerNumber = playerNumber;
            
            //Dealer deals
            Deck gameDeck = new Deck(numberOfDecks);
            Card[] garbage = new Card[gameDeck.deck.Length];
            Dealer dealer = new Dealer(PlayerNumber);
            
            Player[] players = new Player[PlayerNumber];
            
            dealer.DealCards(gameDeck,garbage,PlayerNumber);
            PlayersGetCards(dealer,players);

            bool condition1 = true;
            bool condition2 = true;

            int roundNumber = 1;
            while (condition1 && condition2)
            {
                Console.WriteLine("******************************");
                Console.Write("Round number: ");
                Console.WriteLine(roundNumber);
                condition1 = CheckMoney(players);
                condition2 = CheckCards(PlayerNumber, gameDeck);
                TablePlayRound(gameDeck,garbage,dealer,players,PlayerNumber);
                Console.WriteLine("******************************");
                roundNumber++;
            }
        }

        public void PlayersGetCards(Dealer dealer,Player[] players)
        {
            int rowLength = dealer.cardsDealt.GetLength(0);
            int colLength = dealer.cardsDealt.GetLength(1);
            for (int i = 0; i < rowLength; i++)
            {
                Card[] playerCards = new Card[6];
                for (int j = 0; j < colLength; j++)
                {
                    if (dealer.cardsDealt[i,j] != null)
                    {
                        playerCards[j] = new Card(dealer.cardsDealt[i,j].Value,dealer.cardsDealt[i,j].Suit);
                    }
                }

                if (i == 0)
                {
                    players[i] = new Player(playerCards,true,dealer.cardsDealt,defaultWallet);
                }
                else
                {
                    players[i] = new Player(playerCards,defaultWallet);
                }
            }
        }

        public void PlayerHit(Dealer dealer,Player[] players,int playerIndex)
        {
            Card[] playerCards = new Card[6];
            for (int i = 0; i < playerCards.Length; i++)
            {
                if (dealer.cardsDealt[playerIndex,i] == null)
                {
                    playerCards[i] = null;
                }
                else
                {
                    playerCards[i] = new Card(dealer.cardsDealt[playerIndex,i].Value,dealer.cardsDealt[playerIndex,i].Suit);    
                }
                
                
            }
            
            players[playerIndex].cards = playerCards;
        }

        public void TablePlayRound(Deck gameDeck,Card[] garbage,Dealer dealer,Player[] players,int playerNumber)
        {
            
            
            

            for (int i = 1; i < players.Length; i++)
            {
                bool condition = players[i].PlayerPlays();
                if (condition)
                {
                    while (condition)
                    {
                        dealer.DealOnce(gameDeck,garbage,i);
                        PlayerHit(dealer,players,i);
                        condition = players[i].PlayerPlays();
                    }
                }
            }
            
            bool dealerCondition = players[0].PlayerPlays();
            if (dealerCondition)
            {
                while (dealerCondition)
                {
                    dealer.DealOnce(gameDeck,garbage,0);
                    PlayerHit(dealer,players,0);
                    dealerCondition = players[0].PlayerPlays();
                }
            }
            
            //PrintTable(gameDeck,garbage,players);
            int[] winners = DecidedWinners(players);
            for (int j = 0; j < players.Length; j++)
            {
                // show dealer's hands
                // show players' hands and announce winners
                if (j == 0)
                {
                    Console.WriteLine("Dealer's hand: ");
                    //dealer
                    Player dealerPlayer = players[j];
                    foreach (Card card in dealerPlayer.cards)
                    {
                        if (card != null)
                        {
                            int caseSwitch = card.Value;

                            switch (caseSwitch)
                            {
                                case 11:
                                    Console.Write("Jack");
                                    break;
                                case 12:
                                    Console.Write("Queen");
                                    break;
                                case 13:
                                    Console.Write("King");
                                    break;
                                case 14:
                                    Console.Write("Ace");
                                    break;
                                default:
                                    Console.Write(card.Value);  
                                    break;
                            }
                            Console.Write(" of ");
                            Console.Write(card.Suit);
                            Console.Write(", ");
                            Console.Write("of ");
                            Console.Write(card.Suit);
                            Console.Write(", ");
                        }
                        
                    }

                    int lost = 0;
                    int won = 0;
                    foreach (int number in winners)
                    {
                        if (number == 1)
                        {
                            lost += 1;
                        }
                        else
                        {
                            won += 1;
                        }
                    }

                    int total = (won - lost) * 10;
                    players[j].wallet += total;
                    
                    Console.Write("Total: ");
                    Console.WriteLine(ReturnTotalPoints(dealerPlayer.cards));
                    Console.Write("Casino net: ");
                    Console.WriteLine(players[j].wallet);
                    Console.WriteLine("");
                }
                else
                {
                    //players
                    Player dealerPlayer = players[j];
                    foreach (Card card in dealerPlayer.cards)
                    {
                        if (card != null)
                        {
                            
                            int caseSwitch = card.Value;

                            switch (caseSwitch)
                            {
                                case 11:
                                    Console.Write("Jack");
                                    break;
                                case 12:
                                    Console.Write("Queen");
                                    break;
                                case 13:
                                    Console.Write("King");
                                    break;
                                case 14:
                                    Console.Write("Ace");
                                    break;
                                default:
                                    Console.Write(card.Value);  
                                    break;
                            }
                            Console.Write(" of ");
                            Console.Write(card.Suit);
                            Console.Write(", ");
                        }
                        
                    }
                    Console.Write("Total: ");
                    Console.WriteLine(ReturnTotalPoints(dealerPlayer.cards));

                    if (winners[j] == 1)
                    {
                        Console.WriteLine("Player wins");
                    }
                    else
                    {
                        Console.WriteLine("Player loses");
                    }
                    Console.WriteLine(players[j].wallet);
                }
                
            }
        }

        public int[] DecidedWinners(Player[] players)
        {
            int[] winners = new int[players.Length];
            winners[0] = 2;
            Player dealer = players[0];
            int dealerPoints = ReturnTotalPoints(dealer.cards);
            for (int i = 1; i < players.Length; i++)
            {
                Player player = players[i];
                int playerPoints = ReturnTotalPoints(player.cards);
                if ((playerPoints > dealerPoints && playerPoints < 22) || (dealerPoints > 21 && playerPoints < 22) || playerPoints == 21)
                {
                    winners[i] = 1;
                    players[i].wallet += 10;
                }
                
                
                else
                {
                    winners[i] = 0;
                    players[i].wallet -= 10;
                }
            }
            return winners;
        }

        public int ReturnTotalPoints(Card[] cards)
        {
            
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
            return totalPoints;
        }

        public bool CheckMoney(Player[] players)
        {
            int length = players.Length;
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                if (players[i].wallet >= 0)
                {
                    count++;
                }
            }

            if (count == length)
            {
                // everyone has enough money
                return true;    
            }

            return false;

        }
        public bool CheckCards(int PlayerNumber,Deck gameDeck)
        {
            int limit = PlayerNumber * 52 / 2;
            if (gameDeck.deck.Length > limit)
            {
                // there are enough cards
                return true;
            }
            return false;
        }
        public void PrintTable(Deck gameDeck,Card[] garbage,Player[] players )
        {
            
            Console.Write("Deck length: ");
            Console.WriteLine(gameDeck.deck.Length);
            
            Console.Write("Cards in the garbage: ");
            Console.WriteLine(garbage.Length);
                        
            Console.WriteLine("Player Cards that are dealt \n");
            foreach (Player player in players)
            {
                if (player == null)
                {
                    Console.WriteLine("Not initiated");
                }
                else
                {
                    foreach (Card card in player.cards)
                    {
                        if (card != null)
                        {
                            Console.Write(card.Value);
                            Console.Write(card.Suit);
                            Console.Write(", ");
                        }
                        else
                        {
                            Console.Write("null, ");
                        }
                        
                    }
                    Console.WriteLine("");
                }
                
            }
            
            
        }
    }
}