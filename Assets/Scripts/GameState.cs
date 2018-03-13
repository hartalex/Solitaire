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
		public Pile stockPile;

		public GameObject MenuButton;

		public bool initialized = false;

		// Use this for initialization
		void Start ()
		{
			Deal ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			Deal ();
		}

		void Deal ()
		{
			if (!initialized && deck.initialized) {
				deck.Shuffle (true);
				for (int i = 0; i < tableauPile.GetLength (0); i++) {
					for (int x = 0; x < i + 1; x++) {
						Card card = deck.RemoveCardFromTop ();
						tableauPile [i].AddCardToTop (card, true);

					}
				}
				initialized = true;
			}
		}

		public void Restart ()
		{
			// Return all cards to deck and redeal
			for (int i = 0; i < tableauPile.GetLength (0); i++) {
				MoveCardsFaceDown (tableauPile[i], deck);
			}
			for (int i = 0; i < foundationPile.GetLength (0); i++) {
				MoveCardsFaceDown (foundationPile[i], deck);
			}
			MoveCardsFaceDown (stockPile, deck);
			this.initialized = false;
		}

		private void MoveCardsFaceDown(Pile src, Pile dest) {
			Card card = src.RemoveCardFromTop ();
			while (card != null) {
				card.facingUpNoAnimation = false;
				dest.AddCardToBottom (card, true);
				card = src.RemoveCardFromTop ();
			}
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

		// called when a ui menu is showing so cards can't move around in the background
		public void ShowEverything(bool status) {
			//MenuButton.SetActive (status);
			deck.gameObject.SetActive(status);
			stockPile.gameObject.SetActive(status);
			for (int i = 0; i < tableauPile.GetLength (0); i++) {
				tableauPile[i].gameObject.SetActive(status);
			}
			for (int i = 0; i < foundationPile.GetLength (0); i++) {
				foundationPile[i].gameObject.SetActive(status);
			}
		}


			
	}
}