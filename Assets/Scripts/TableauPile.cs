using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire {
	public class TableauPile : Pile {

		public TableauPile() : base()
		{

		}
		public void AddCardToEnd(Card card)
		{

			base.AddCardToEnd (card);

			card.transform.localPosition = new Vector3 (0,-(size-1) * 0.1f,-(size-1) * cardThickness);

		}

		public void AddCardToStart(Card card)
		{
			base.AddCardToStart (card);
			card.transform.localPosition = new Vector3 (0,-(size-1) * 0.1f,-(size-1) * cardThickness);

		}
	}
}