using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire {
public class GameState : MonoBehaviour {

	public Card card;
		public Pile pile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			if (pile.GetSize () == 0) {
				pile.AddCardToEnd (card);
			}
	}
}
}