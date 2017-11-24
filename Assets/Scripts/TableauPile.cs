using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire {
	public class TableauPile : Pile {

		public TableauPile() : base()
		{

		}


		public bool AddCard(Card card)
		{
			bool retval = false;
			return retval;
		}

		public new void AddCardToTop(Card card)
		{
			base.AddCardToTop (card);
			for (int i = 0; i < size; i++) {
				cards[i].transform.localPosition = new Vector3 (0, -i * 0.1f,  -(i+1) * cardThickness);
			}

		}

		public new void AddCardToBottom(Card card)
		{
			base.AddCardToBottom (card);
			card.transform.localPosition = new Vector3 (0,-(size-1) * 0.1f,-(size-1) * cardThickness);

		}

		public void Update() {
			Card topCard = GetCardFromTop ();
			if (topCard != null && topCard.facingUp == false) {
				topCard.facingUp = true;

			}
		}

		public void AddPile(Card[] newcards)
		{
			int i = 51;
			while (i>= 0) {
				if (newcards [i] != null) {
					AddCardToTop (newcards [i]);
				}
				i--;
			}

		}
	}
}