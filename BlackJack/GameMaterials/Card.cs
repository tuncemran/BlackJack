namespace BlackJack
{
    public class Card
    {
        public int Value;
        public static string[] SuitsArray = new string[] {"Hearts","Diamonds", "Clubs", "Spades"};
        public string Suit;

        public Card(int value, string suit)
        {
            
            Value = value;
            Suit = suit;
        }
        
    }
}