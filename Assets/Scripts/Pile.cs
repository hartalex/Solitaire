using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Solitaire
{
    
	// End = Top of Pile,  Start = Bottom of Pile
	public class Pile: MonoBehaviour
    {
		public GameObject cardprefab = null;
        private Card[] cards;
        private int size = 0;
		private float cardThickness  = 1f;

        public Pile()
        {
			cards = new Card[52];
        }

		public int GetSize() {
			return size;
		}

        public void AddCardToEnd(Card card)
        {
			

            cards[size] = card;
			// Sets display
			card.transform.SetParent (this.transform);
			card.transform.localPosition = new Vector3 ();
			for (int i = size; i >= 0; i--)
			{
				cards[i].transform.localPosition = new Vector3 (0,0,(size-i) * cardThickness);
			}
            size++;
		

        }

        public void AddCardToStart(Card card)
        {
            for (int i = size; i > 0; i--)
            {
                cards[i] = cards[i-1];
            }
			size++;
			cards[0] = card;
			card.transform.SetParent (this.transform);
			card.transform.localPosition = new Vector3 (0,0,size * cardThickness);
            
        }

        public Card RemoveCardFromEnd()
        {
            Card retval = GetCardFromEnd();
            if (retval != null)
            {
				
                size--;
				retval.transform.SetParent (null);
				for (int i = size; i >= 0; i--)
				{
					cards[i].transform.localPosition = new Vector3 (0,0,(size -i) * cardThickness);
				}
            }
            return retval;
        }

        public Card GetCardFromEnd()
        {
            Card retval = null;
            if (size > 0)
            {
                retval = cards[size - 1];
            }
            return retval;
        }

        public Card RemoveCardFromStart()
        {
            Card retval = null;
            if (size > 0)
            {
                retval = cards[0];
				retval.transform.SetParent (null);
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

        public Pile GetCardsFromStart(int size)
        {
            Pile pile = new Pile();
            for (int i = 0; i < size; i++)
            {
                pile.AddCardToEnd(this.RemoveCardFromStart());
            }
            return pile;
        }

        public Pile GetCardsFromEnd(int size)
        {
            Pile pile = new Pile();
            for (int i = 0; i < size; i++)
            {
                pile.AddCardToEnd(this.RemoveCardFromEnd());
            }
            return pile;
        }

        public Card GetCardFromStart()
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
					cards [i].transform.localPosition = new Vector3 (0, 0, (size -1 - i) * cardThickness);
				}
			}
        }

        public void Shuffle()
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
				for (int i = size-1; i >= 0; i--) {
				
					cards [i].transform.localPosition = new Vector3 (0, 0, (size-1 - i) * cardThickness);
				}
			}
        }

        public void AddPile(Pile pile)
        {
            for (int i = 0; i < pile.size; i++)
            {
                Card card = pile.GetCard(i);
                this.AddCardToEnd(card);
            }
        }

		protected Card CreateCard(Suit suit, Rank rank) {
			Card retval = null;
			GameObject gameObjectCard = GameObject.Instantiate(cardprefab);

			retval = gameObjectCard.GetComponent<Card> ();
			retval.suit = suit;
			retval.rank = rank;
			gameObjectCard.name = retval.ToShortName ();
			return retval;
		}
    }
}
