﻿using System.Collections;
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
			bool first = false;
			int firstFacingUpIndex = -1;
			for (int i = 0; i < size; i++) {
				if (cards [i].facingUp && first != true) {
					first = true;
					firstFacingUpIndex = i;
					cards [i].MoveTo( new Vector3 (0, -i * 0.15f, -(i + 1) * cardThickness));
				} else if (cards [i].facingUp && first) {
							cards [i].MoveTo( new Vector3 (0, -firstFacingUpIndex * 0.15f + -(i-firstFacingUpIndex) * 0.3f, -(i + 1) * cardThickness));
				} else {
									cards [i].MoveTo(new Vector3 (0, -i * 0.15f, -(i + 1) * cardThickness));
				}
			}

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