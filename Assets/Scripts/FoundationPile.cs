using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace Solitaire
{
    public class FoundationPile : Pile
    {
        private bool isFull;
        public FoundationPile() : base()
        {

        }

        public bool AddCard(Card card)
        {
			bool retval = IsValidMove(card);
			if (retval)
			{
				AddCardToPile(card);
			}
            return retval;
        }

		public bool IsValidMove(Card card)
		{
			bool retval = false;
            if (this.GetSize() == 0)
            {
                // Accept any Ace first
                if (card.rank == Rank.Ace)
				{
                    retval = true;
                }
            }
            else
            {
                Card TopCard = this.GetCardFromTop();
                if (TopCard.suit == card.suit && (int)TopCard.rank == ((int)card.rank) - 1)
                {              
                    retval = true;
                }
            }
            return retval;
		}

        public bool IsFull()
        {
            return isFull;
        }

		protected void AddCardToPile(Card card)
		{
			card.facingUp = true;
            Collider col = card.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true;
            }
            AddCardToTop(card);
			Card TopCard = this.GetCardFromTop();
			if (TopCard.rank == Rank.King)
			{
				isFull = true;
			}
		}
    }
}
