﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Solitaire
{
    
	// Top = Top of Pile,  Top = Bottom of Pile
	public class Pile: MonoBehaviour
    {
        protected Card[] cards;
		protected int size = 0;
		protected float cardThickness  = 0.008f;

        public Pile()
        {
			cards = new Card[52];
        }

		public int GetSize() {
			return size;
		}

		public void AddCardToTop(Card card, bool noMove)
		{
			cards[size] = card;
			// Sets display
			card.transform.SetParent (this.transform);

			for (int i = 0; i <= size; i++) {
				if (noMove) {
					cards [i].SetPosition(new Vector3 (0, 0, -(i + 1) * cardThickness));
				} else {
					cards [i].MoveTo (new Vector3 (0, 0, -(i + 1) * cardThickness));
				}
			}
			size++;
		}

        public void AddCardToTop(Card card)
        {
			this.AddCardToTop (card, false);
        }

		public void AddCardToBottom(Card card, bool noMove)
        {
            for (int i = size; i > 0; i--)
            {
                cards[i] = cards[i-1];
            }
			size++;
			cards[0] = card;
			if (this != null) {
				card.transform.SetParent (this.transform);
			}
			if (noMove) {
				card.SetPosition(new Vector3 (0,0,-size * cardThickness));
			} else {
				card.MoveTo(new Vector3 (0,0,-size * cardThickness));
			}
      
        }

		public void AddCardToBottom(Card card)
		{
			this.AddCardToBottom (card, false);
		}

        public Card RemoveCardFromTop()
        {
            Card retval = GetCardFromTop();
            if (retval != null)
            {
				size--;
				cards [size] = null;
            
				//retval.transform.SetParent (null);
			
            }
            return retval;
        }

        public Card GetCardFromTop()
        {
            Card retval = null;
            if (size > 0)
            {
                retval = cards[size - 1];
            }
            return retval;
        }

        public Card RemoveCardFromBottom()
        {
            Card retval = null;
            if (size > 0)
            {
                retval = cards[0];
				//retval.transform.SetParent (null);
                size--;
                for (int i = 0; i < size; i++)
                {
                    cards[i] = cards[i + 1];
				
                }
                
            }
            return retval;
        }
        public Card GetCard(int index)
        {
            Card retval = null;
            if (index >= 0 && index <= size)
            {
                retval = cards[index];
            }
            return retval;
        }

        public Pile GetCardsFromBottom(int size)
        {
            Pile pile = new Pile();
            for (int i = 0; i < size; i++)
            {
                pile.AddCardToTop(this.RemoveCardFromBottom());
            }
            return pile;
        }

        public Pile GetCardsFromTop(int size)
        {
            Pile pile = new Pile();
            for (int i = 0; i < size; i++)
            {
                pile.AddCardToTop(this.RemoveCardFromTop());
            }
            return pile;
        }

        public Card GetCardFromBottom()
        {
            Card retval = null;
            if (size > 0)
            {
                retval = cards[0];
            }
            return retval;
        }

        public void Order()
        {
			if (size > 0) {
				Array.Sort (cards);
				for (int i = size; i >= 0; i--) {
					cards [i].MoveTo(new Vector3 (0, cards[i].transform.localPosition.y, -(size -1 - i) * cardThickness));
				}
			}
        }

		public void Shuffle(bool noMove)
        {
			if (size > 0) {
				int n = size;
				System.Random rng = new System.Random ();
				while (n > 1) {
					int k = rng.Next (n--);
					Card temp = cards [n];
					cards [n] = cards [k];
					cards [k] = temp;
				}
				for (int i = 0; i < size; i++) {
					if (noMove) {
						cards [i].SetPosition(new Vector3 (0, 0, -(i + 1) * cardThickness));
					} else {
						cards [i].MoveTo (new Vector3 (0, 0, -(i + 1) * cardThickness));
					}
				}
			}
        }

		public void Shuffle() {
			this.Shuffle (false);
		}

		public Card[] SplitPileAtCard(Card card) {
			Card[] retval = new Card[52];
			int i = size -1;
			int ni = -1;
			while (i >= 0 && cards [i] != null && (cards[i].rank != card.rank || cards[i].suit != card.suit)) {
				i--;
				retval[++ni] = RemoveCardFromTop ();
			}
			if (i >= 0) {
				if (cards [i].rank == card.rank && cards [i].suit == card.suit) {
					retval [++ni] = RemoveCardFromTop ();
				}
			}

			return retval;
		}

		public Card[] GetPileAtCard(Card card) {
			Card[] retval = new Card[52];
			int i = size -1;
			int ni = -1;
			while (i >= 0 && cards [i] != null && (cards[i].rank != card.rank || cards[i].suit != card.suit)) {
				ni++;
				retval[ni] = cards[i];
				i--;
			}
			if (cards[i].rank == card.rank && cards[i].suit == card.suit)
			{
				retval[++ni] = cards[i];
			}
			return retval;
		}
    }
}
