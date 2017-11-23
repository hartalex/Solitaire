using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Solitaire {
public class GameState : MonoBehaviour {


	public Deck deck;
	public TableauPile[] tableauPile;
	public bool initialized = false;
	// Use this for initialization
	void Start () {
			Init ();
	}
	
	// Update is called once per frame
	void Update () {
			Init ();
	}

	void Init() {
		if (!initialized && deck.initialized) {
			deck.Shuffle ();
			for (int i = 0; i < tableauPile.GetLength (0); i++) {
				for (int x = 0; x < i + 1; x++) {
					Card card = deck.RemoveCardFromTop();
					tableauPile [i].AddCardToTop (card);
				}
			}
			initialized = true;
		}
	}

		public void Restart() {
			SceneManager.LoadScene (0);
		}
}
}