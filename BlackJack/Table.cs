using System;

namespace BlackJack
{
    public class Table
    {
        int PlayerNumber;

        public Table(int playerNumber, int numberOfDecks)
        {
            // Number of players 
            PlayerNumber = playerNumber;
            
            //Dealer deals
            Deck gameDeck = new Deck(numberOfDecks);
            Card[] garbage = new Card[gameDeck.deck.Length];
            Dealer dealer = new Dealer(PlayerNumber);
            
            Player[] players = new Player[PlayerNumber];



            /*//PrintTable(gameDeck,garbage,players);
            dealer.DealCards(gameDeck,garbage,PlayerNumber);
            PlayersGetCards(dealer,players);
            PrintTable(gameDeck,garbage,players);
            //bool dealer1 = players[0].PlayerPlays();
            //bool player1 = players[1].PlayerPlays();
            //Console.WriteLine(dealer1);
            //Console.WriteLine(player1);
            dealer.DealOnce(gameDeck,garbage,1);
            PlayerHit(dealer,players,1);
            PrintTable(gameDeck,garbage,players);*/
            
            
            TablePlayRound(gameDeck,garbage,dealer,players,PlayerNumber);
            
            
            //Player plays
            //Dealer plays
            //Winners are decided
            //Cards are added to the garbage
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
                    players[i] = new Player(playerCards,true,dealer.cardsDealt);
                }
                else
                {
                    players[i] = new Player(playerCards);
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
            
            dealer.DealCards(gameDeck,garbage,PlayerNumber);
            PlayersGetCards(dealer,players);
            //PrintTable(gameDeck,garbage,players);

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
                    Console.Write("Total: ");
                    Console.WriteLine(ReturnTotalPoints(dealerPlayer.cards));
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
                }
                
                
                else
                {
                    winners[i] = 0;
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