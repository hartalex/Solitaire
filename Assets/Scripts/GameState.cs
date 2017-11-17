using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire {
public class GameState : MonoBehaviour {

	public Card card;
		public Pile pile;
		public Pile deck;
	// Use this for initialization
	void Start () {
			deck.Shuffle ();
	}
	
	// Update is called once per frame
	void Update () {
			
	}
}
}