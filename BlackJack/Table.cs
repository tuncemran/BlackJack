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
            PrintTable(gameDeck,garbage,players);

            for (int i = 0; i < players.Length; i++)
            {
                bool condition = players[i].PlayerPlays();
                if (condition)
                {
                    Console.WriteLine("Player played \n");
                    dealer.DealOnce(gameDeck,garbage,i);
                    PlayerHit(dealer,players,i);
                }
            }
            
            PrintTable(gameDeck,garbage,players);
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