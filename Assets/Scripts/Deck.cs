using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Solitaire
{
    public class Deck : Pile 
    {
		public bool initialized = false;

		public Material[] materials;
        public Deck() : base()
		{
		}

		public void Init() {
			if (!initialized) {
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Ace, materials[0]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Two, materials[1]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Three, materials[2]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Four, materials[3]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Five, materials[4]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Six, materials[5]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Seven, materials[6]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Eight, materials[7]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Nine, materials[8]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Ten, materials[9]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Jack, materials[10]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Queen, materials[11]));
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.King, materials[12]));

				this.AddCardToTop(CreateCard(Suit.Club, Rank.Ace, materials[13]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Two, materials[14]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Three, materials[15]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Four, materials[16]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Five, materials[17]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Six, materials[18]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Seven, materials[19]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Eight, materials[20]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Nine, materials[21]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Ten, materials[22]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Jack, materials[23]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Queen, materials[24]));
				this.AddCardToTop(CreateCard(Suit.Club, Rank.King, materials[25]));

				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Ace, materials[26]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Two, materials[27]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Three, materials[28]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Four, materials[29]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Five, materials[30]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Six, materials[31]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Seven, materials[32]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Eight, materials[33]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Nine, materials[34]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Ten, materials[35]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Jack, materials[36]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Queen, materials[37]));
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.King, materials[38]));

				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Ace, materials[39]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Two, materials[40]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Three, materials[41]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Four, materials[42]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Five, materials[43]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Six, materials[44]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Seven, materials[45]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Eight, materials[46]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Nine, materials[47]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Ten, materials[48]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Jack, materials[49]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Queen, materials[50]));
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.King, materials[51]));
				//this.Shuffle ();
				initialized = true;
			}
		}

		public void Start() {
			Init ();
        }

		public void Update() {
			Init ();
		}

    }
}
