﻿using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	private SimpleGestureListener GestureListener;

	// Use this for initialization
	void Start () {
		GestureListener = Camera.main.GetComponent<SimpleGestureListener>();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 grabVector = GestureListener.grabVector * 10000;

		if (GestureListener.grabbing) {
			transform.Rotate(0, grabVector.x * Time.deltaTime, 0, Space.Self);
			transform.Rotate(grabVector.y * Time.deltaTime, 0, 0, Space.World);
		}
	}
}
