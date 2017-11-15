using System;
using System.Collections.Generic;
using System.Text;

namespace Solitaire
{
    public class Deck : Pile 
    {
        public Deck() : base()
        {
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Ace));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Two));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Three));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Four));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Five));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Six));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Seven));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Eight));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Nine));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Ten));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Jack));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.Queen));
            this.AddCardToEnd(new Card(Suit.Diamond, Rank.King));

            this.AddCardToEnd(new Card(Suit.Club, Rank.Ace));
            this.AddCardToEnd(new Card(Suit.Club, Rank.Two));
            this.AddCardToEnd(new Card(Suit.Club, Rank.Three));
            this.AddCardToEnd(new Card(Suit.Club, Rank.Four));
            this.AddCardToEnd(new Card(Suit.Club, Rank.Five));
            this.AddCardToEnd(new Card(Suit.Club, Rank.Six));
            this.AddCardToEnd(new Card(Suit.Club, Rank.Seven));
            this.AddCardToEnd(new Card(Suit.Club, Rank.Eight));
            this.AddCardToEnd(new Card(Suit.Club, Rank.Nine));
            this.AddCardToEnd(new Card(Suit.Club, Rank.Ten));
            this.AddCardToEnd(new Card(Suit.Club, Rank.Jack));
            this.AddCardToEnd(new Card(Suit.Club, Rank.Queen));
            this.AddCardToEnd(new Card(Suit.Club, Rank.King));

            this.AddCardToEnd(new Card(Suit.Heart, Rank.Ace));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.Two));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.Three));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.Four));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.Five));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.Six));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.Seven));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.Eight));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.Nine));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.Ten));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.Jack));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.Queen));
            this.AddCardToEnd(new Card(Suit.Heart, Rank.King));

            this.AddCardToEnd(new Card(Suit.Spade, Rank.Ace));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.Two));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.Three));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.Four));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.Five));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.Six));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.Seven));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.Eight));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.Nine));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.Ten));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.Jack));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.Queen));
            this.AddCardToEnd(new Card(Suit.Spade, Rank.King));
        }
    }
}
