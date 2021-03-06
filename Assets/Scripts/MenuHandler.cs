﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Solitaire
{
	
public class MenuHandler : MonoBehaviour {

	public GameState Gamestate;
		public GameObject Menu;
		protected bool hidden = true;
        public StatManager statManager;
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
            statManager.IncrementStat("TableFlips");
			SceneManager.LoadScene ("TableFlip");
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

		public void QuitButtonClick() {
			Debug.Log ("QuitButtonClick");
			#if UNITY_EDITOR
			// Application.Quit() does not work in the editor so
			// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit();
			#endif
		}


}
}
