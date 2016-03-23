using UnityEngine;
using System.Collections;
using System;

public class SimpleGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	public float nearMax;
	public float farMax;
	public bool grabbing;
	public Vector2 grabVector;
	private Vector2 previousGrabVector;
	public GameObject Cursor;
	
	public void UserDetected(uint userId, int userIndex)
	{
		// as an example - detect these user specific gestures
		KinectManager manager = KinectManager.Instance;

		manager.DetectGesture(userId, KinectGestures.Gestures.ZoomIn);
		manager.DetectGesture(userId, KinectGestures.Gestures.ZoomOut);
	}
	
	public void UserLost(uint userId, int userIndex)
	{

	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		//GestureInfo.guiText.text = string.Format("{0} Progress: {1:F1}%", gesture, (progress * 100));
		if ((gesture == KinectGestures.Gestures.ZoomIn) /*&& progress > 0.25f*/ && Camera.main.transform.localPosition.z <= -nearMax) {
			Camera.main.transform.Translate (new Vector3 (0, 0, progress * 0.2f));
			Debug.Log ("ZoomIn");
		} else if ((gesture == KinectGestures.Gestures.ZoomOut) /*&& progress > 0.25f*/ && Camera.main.transform.localPosition.z >= -farMax) {
			Camera.main.transform.Translate (new Vector3 (0, 0, progress * -0.2f));
			Debug.Log ("ZoomOut");
		} else if ((gesture == KinectGestures.Gestures.Grab) && progress == 1f) {
			grabVector = new Vector2 (Cursor.transform.position.x - previousGrabVector.x, Cursor.transform.position.y - previousGrabVector.y); 
			previousGrabVector = new Vector2 (Cursor.transform.position.x, Cursor.transform.position.y);
			grabbing = true;
			Debug.Log ("Grab");
		} else {
			grabbing = false;
		}
	}

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		return true;
	}
	
}
