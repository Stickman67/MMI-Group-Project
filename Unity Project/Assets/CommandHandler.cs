using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandHandler : MonoBehaviour {
	
	enum EColour
	{
		Red, Green, Blue, Yellow, Orange, Purple, Pink, Black, White
	};

	struct Marker
	{
		public Marker(Quaternion newEarthRotation, int newId, EColour newColour)
		{
			earthRotation = newEarthRotation;
			id = newId;
			colour = newColour;
		}

		Quaternion earthRotation;
		int id;
		EColour colour;
	}

	public Queue<string> commandQueue;
	
	public GameObject markerPrefab;
	public GameObject earth;
	private List<Marker> markers;

	// Use this for initialization
	void Start () {
		commandQueue = new Queue<string> ();
		markers = new List<Marker> ();
	}
	
	// Update is called once per frame
	void Update () {
		while (commandQueue.Count != 0)
		{
			string[] cmd = commandQueue.Dequeue().Split(' ');

			if (cmd[0] == "place" || cmd[0] == "create"  || cmd[0] == "add" )
			{
				GameObject instance = (GameObject)Instantiate(markerPrefab, new Vector3(0, 0, -20), new Quaternion(0, 0, 0, 0));
				instance.transform.parent = earth.transform;

				if (cmd.Length == 2)
				{
					instance.renderer.material.color = new Color(1, 0, 0);
					markers.Add(new Marker(earth.transform.rotation, 1, EColour.Red));
				}
				else if (cmd.Length == 3)
				{
					switch (cmd[1])
					{
					case "red":
						instance.renderer.material.color = new Color(1, 0, 0);
						markers.Add(new Marker(earth.transform.rotation, 1, EColour.Red));
						break;
					case "green":
						instance.renderer.material.color = new Color(0, 1, 0);
						markers.Add(new Marker(earth.transform.rotation, 1, EColour.Green));
						break;
					case "blue":
						instance.renderer.material.color = new Color(0, 0, 1);
						markers.Add(new Marker(earth.transform.rotation, 1, EColour.Blue));
						break;
					case "yellow":
						instance.renderer.material.color = new Color(1, 1, 0);
						markers.Add(new Marker(earth.transform.rotation, 1, EColour.Yellow));
						break;
					case "orange":
						instance.renderer.material.color = new Color(1, 0.5f, 0);
						markers.Add(new Marker(earth.transform.rotation, 1, EColour.Orange));
						break;
					case "purple":
						instance.renderer.material.color = new Color(1, 0, 1);
						markers.Add(new Marker(earth.transform.rotation, 1, EColour.Purple));
						break;
					case "pink":
						instance.renderer.material.color = new Color(1, 0, 0.5f);
						markers.Add(new Marker(earth.transform.rotation, 1, EColour.Pink));
						break;
					case "black":
						instance.renderer.material.color = new Color(0, 0, 0);
						markers.Add(new Marker(earth.transform.rotation, 1, EColour.Black));
						break;
					case "white":
						instance.renderer.material.color = new Color(1, 1, 1);
						markers.Add(new Marker(earth.transform.rotation, 1, EColour.White));
						break;
					default:
						instance.renderer.material.color = new Color(1, 0, 0);
						markers.Add(new Marker(earth.transform.rotation, 1, EColour.Red));
						break;
					}
				}
			}
		}
	}
}
