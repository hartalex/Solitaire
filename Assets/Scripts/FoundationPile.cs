using System;
using System.Collections.Generic;
using System.Text;


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
            bool retval = false;
			if (this.GetSize() == 0)
            {
                // Accept any Ace first
                if (card.rank == Rank.Ace)
                {
                    card.facingUp = true;
					AddCardToTop(card);
                    retval = true;
                }
            }
            else
            {
                Card TopCard = this.GetCardFromTop();
                if (TopCard.suit == card.suit && (int)TopCard.rank == ((int)card.rank)-1)
                {
                    card.facingUp = true;
					AddCardToTop(card);
                    retval = true;
                    if (TopCard.rank == Rank.King)
                    {
                        isFull = true;
                    }
                } 
                
            }
            return retval;
        }

        public bool IsFull()
        {
            return isFull;
        }
    }
}
