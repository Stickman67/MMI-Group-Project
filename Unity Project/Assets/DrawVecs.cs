using UnityEngine;
using System.Collections;

public class DrawVecs : MonoBehaviour {

	void OnDrawGizmos()
	{
		Color color;
		color = Color.green;
		// local up
		DrawHelperAtCenter(this.transform.up, color, 100f);
		
		color.g -= 0.5f;
		// global up
		DrawHelperAtCenter(Vector3.up, color, 100f);
		
		color = Color.blue;
		// local forward
		DrawHelperAtCenter(this.transform.forward, color, 100f);
		
		color.b -= 0.5f;
		// global forward
		DrawHelperAtCenter(Vector3.forward, color, 100f);
		
		color = Color.red;
		// local right
		DrawHelperAtCenter(this.transform.right, color, 100f);
		
		color.r -= 0.5f;
		// global right
		DrawHelperAtCenter(Vector3.right, color, 100f);
	}
	
	private void DrawHelperAtCenter(
		Vector3 direction, Color color, float scale)
	{
		Gizmos.color = color;
		Vector3 destination = transform.position + direction * scale;
		Gizmos.DrawLine(transform.position, destination);
	}
}
