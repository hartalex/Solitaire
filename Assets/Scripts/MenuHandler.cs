using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Solitaire
{
	
public class MenuHandler : MonoBehaviour {

	public GameState Gamestate;
		public GameObject Menu;
		protected bool hidden = true;
	// Use this for initialization
	void Start () {
		
	}
	
	public void MenuButtonClick() {
			if (hidden) {
				this.Show ();
			} else {
				this.Hide ();
			}
	}

		// Update is called once per frame
	public void Show () {
		hidden = false;
			Menu.SetActive (true);
		Gamestate.ShowEverything (false);
	}

	public void Hide() {
			hidden = true;
			Menu.SetActive (false);
		Gamestate.ShowEverything (true);
	}

		public void NewGameButtonClick() {
			Debug.Log ("NewGameButtonClick");
			this.Hide ();
			this.Gamestate.Restart ();
		}

		public void SettingsButtonClick() {
			Debug.Log ("SettingsButtonClick");
		}

		public void StoreButtonClick() {
			Debug.Log ("StoreButtonClick");
		}

		public void AchievementsButtonClick() {
			Debug.Log ("AchievementsButtonClick");
		}

}
}
