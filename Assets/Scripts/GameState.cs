using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solitaire
{
	public class GameState : MonoBehaviour
	{


		public Deck deck;
		public TableauPile[] tableauPile;
		public FoundationPile[] foundationPile;
		public bool initialized = false;
		// Use this for initialization
		void Start ()
		{
			Init ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			Init ();
		}

		void Init ()
		{
			if (!initialized && deck.initialized) {
				deck.Shuffle (true);
				for (int i = 0; i < tableauPile.GetLength (0); i++) {
					for (int x = 0; x < i + 1; x++) {
						Card card = deck.RemoveCardFromTop ();
						tableauPile [i].AddCardToTop (card);

					}
				}
				initialized = true;
			}
		}

		public void Restart ()
		{
			SceneManager.LoadScene (0);
		}

		public bool AutoPlaceCard (Card card)
		{
			bool cardPlaced = false;
			Pile srcPile = card.GetComponentInParent<TableauPile> ();
			if (srcPile == null) {
				srcPile = card.GetComponentInParent<FoundationPile> ();
				if (srcPile == null) {
					srcPile = card.GetComponentInParent<Pile> ();
				}
			}

			if (srcPile != null) {
				for (int i = 0; !cardPlaced && i < foundationPile.GetLength (0); i++) {
					if (foundationPile [i].AddCard (card)) {
						srcPile.RemoveCardFromTop ();
						cardPlaced = true;
					}
				}
				for (int i = 0; !cardPlaced && i < tableauPile.GetLength (0); i++) {
					if (tableauPile [i].GetSize () == 0 && card.rank == Rank.King) {
						tableauPile [i].AddPile (srcPile.SplitPileAtCard (card));
						cardPlaced = true;
					} else if (tableauPile [i].GetSize () > 0) {
						Card topCard = tableauPile [i].GetCardFromTop ();
						if (topCard.GetColor () != card.GetColor () &&
						    ((int)topCard.rank) - 1 == ((int)card.rank) && card.rank != Rank.Ace) {
							tableauPile [i].AddPile (srcPile.SplitPileAtCard (card));
							cardPlaced = true;
						}
					}
				}



			}
			return cardPlaced;
		}
	}
}