using System;

namespace BlackJack
{
    public class Deck
    {
        public Card[] deck;
        public Deck(int number_of_decks)
        {
            deck = new Card[52 * number_of_decks];
            FillDeck(deck,number_of_decks);
            ShuffleDeck(deck);
        }

        public static void FillDeck(Card[] deck,int number_of_decks)
        {
            int index = 0;
            for (int i = 0; i < number_of_decks; i++)
            {
                foreach (string suit in Card.SuitsArray)
                {
                    for (int value = 2; value <= 14; value++)
                    {
                        Card card = new Card(value,suit);
                        deck[index] = card;
                        index++;
                    }
                }    
            }
            
        }

        public static void ShuffleDeck(Card[] deck)
        {
            Random rand = new Random();

            for (int i = 0; i < deck.Length - 1; i++)
            {
                int j = rand.Next(i, deck.Length);
                Card card = deck[i];
                deck[i] = deck[j];
                deck[j] = card;

            }
        }

        public static void PrintDeck(Card[] deck)
        {
            for (int i = 0; i < 52; i++)
            {
                Console.WriteLine($"{deck[i].Value} {deck[i].Suit}");
            }
        }

        
        
    }
}