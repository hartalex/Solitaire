﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction {
	x,y,z}

public class MaxHeightScript : MonoBehaviour {

	private double maximumHeight = double.MinValue;
	public Text uiText;
	public HeightDirection direction = HeightDirection.z;
	// Use this for initialization
	void Start () {
		maximumHeight = double.MinValue;
	}
	
	// Update is called once per frame
	void Update () {
		double height;
		switch(direction) {
		case HeightDirection.x: 
				height = transform.position.x;
				break;
		case HeightDirection.y: 
				height = transform.position.y;
				break;
		default:
		case HeightDirection.z: 
				height = transform.position.z;
				break;
		}
		if (height > maximumHeight) {
			maximumHeight = height;
			uiText.text = maximumHeight.ToString("F");
		}
	}
}
