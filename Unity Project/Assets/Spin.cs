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
		
		Vector2 grabVector = GestureListener.grabVector * 1000 * -hand.transform.position.z;
		GestureListener = hand.GetComponent<SimpleGestureListener>();

		if (GestureListener.grabbing) {
			//Vector3 rotationAxis = Vector3.Cross(grabVector.normalized, Vector3.forward);
			//transform.RotateAround(transform.position, rotationAxis, Mathf.Sin(grabVector.magnitude*2*Mathf.PI)*Mathf.Rad2Deg);
			transform.Rotate(transform.up, -grabVector.x * Time.deltaTime);
			transform.Rotate(Vector3.left, grabVector.y * Time.deltaTime);
		}
	}
}
