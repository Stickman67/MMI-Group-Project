using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
	
	public GameObject hand;
	private SimpleGestureListener GestureListener;

	// Use this for initialization
	void Start () {
		GestureListener = hand.GetComponent<SimpleGestureListener>();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 grabVector = GestureListener.grabVector * 100;
		GestureListener = hand.GetComponent<SimpleGestureListener>();

		if (GestureListener.grabbing) {
			transform.RotateAround(transform.up, -grabVector.x * Time.deltaTime);
			transform.RotateAround(Vector3.left, grabVector.y * Time.deltaTime);
		}
	}
}
