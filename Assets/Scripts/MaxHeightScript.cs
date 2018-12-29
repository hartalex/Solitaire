using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction {
	x,y,z}

public class MaxHeightScript : MonoBehaviour {

	public double maximumHeight = double.MinValue;
	public Text uiText;
	public Direction direction = Direction.z;
	// Use this for initialization
	void Start () {
		maximumHeight = double.MinValue;
	}
	
	// Update is called once per frame
	void Update () {
		double height;
		switch(direction) {
			case Direction.x: 
				height = transform.position.x;
				break;
			case Direction.y: 
				height = transform.position.y;
				break;
		default:
			case Direction.z: 
				height = transform.position.z;
				break;
		}
		if (height > maximumHeight) {
			maximumHeight = height;
			uiText.text = maximumHeight.ToString("F");
		}
	}
}
