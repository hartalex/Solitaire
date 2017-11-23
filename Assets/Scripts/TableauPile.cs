using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire {
	public class TableauPile : Pile {

		public TableauPile() : base()
		{

		}


		public void AddCardToTop(Card card)
		{
			base.AddCardToTop (card);

			for (int i = 0; i < size; i++) {
				cards[i].transform.localPosition = new Vector3 (0, -i * 0.1f,  -i * cardThickness);
			}

		}

		public void AddCardToBottom(Card card)
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
	}
}