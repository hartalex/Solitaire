using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Solitaire
{
    public class Deck : Pile 
    {
		public GameObject cardprefab = null;
        public Deck() : base()
		{
		}
		public void Start() {
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Ace));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Two));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Three));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Four));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Five));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Six));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Seven));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Eight));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Nine));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Ten));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Jack));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.Queen));
            this.AddCardToEnd(CreateCard(Suit.Diamond, Rank.King));

            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Ace));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Two));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Three));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Four));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Five));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Six));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Seven));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Eight));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Nine));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Ten));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Jack));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.Queen));
            this.AddCardToEnd(CreateCard(Suit.Club, Rank.King));

            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Ace));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Two));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Three));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Four));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Five));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Six));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Seven));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Eight));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Nine));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Ten));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Jack));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.Queen));
            this.AddCardToEnd(CreateCard(Suit.Heart, Rank.King));

            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Ace));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Two));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Three));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Four));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Five));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Six));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Seven));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Eight));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Nine));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Ten));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Jack));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.Queen));
            this.AddCardToEnd(CreateCard(Suit.Spade, Rank.King));
        }

		private Card CreateCard(Suit suit, Rank rank) {
			Card retval = null;
			GameObject gameObjectCard = GameObject.Instantiate(cardprefab);
			retval = gameObjectCard.GetComponent<Card> ();
			retval.suit = suit;
			retval.rank = rank;

			return retval;
		}
    }
}
