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



            //PrintTable(gameDeck,garbage,players);
            dealer.DealCards(gameDeck,garbage,PlayerNumber);
            PlayersGetCards(dealer,players);
            //PrintTable(gameDeck,garbage,players);
            bool dealer1 = players[0].PlayerPlays();
            //bool player1 = players[1].PlayerPlays();
            Console.WriteLine(dealer1);
            //Console.WriteLine(player1);
            
            
               
            
            
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
                            Console.Write(card.Suit, ",");    
                        }
                        else
                        {
                            Console.Write("null");
                        }
                        
                    }
                    Console.WriteLine("");
                }
                
            }
            
            
        }
    }
}