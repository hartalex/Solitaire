using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TableFlipState : MonoBehaviour {

	public float waitToDispPlayAgain = 5;
	protected float startTime = 0;
	public float currentTime;
	public GameObject PlayAgainText;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (!PlayAgainText.activeSelf) {
			currentTime = Time.time - startTime;
			if (currentTime > waitToDispPlayAgain) {
				PlayAgainText.SetActive (true);
			}
		}

		if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
			SceneManager.LoadScene ("game.scene");
		}
	}
}
