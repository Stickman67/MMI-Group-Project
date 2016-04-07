using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandHandler : MonoBehaviour {
	
	struct Marker
	{
		public Marker(Quaternion newEarthRotation, int newId, GameObject marker)
		{
			earthRotation = newEarthRotation;
			id = newId;
			colour = marker.renderer.material.color;
			instance = marker;
		}

		public Quaternion earthRotation;
		public int id;
		public Color colour;
		public GameObject instance;

		public static bool operator ==(Marker m1, Marker m2)
		{
			if (m1.colour == m2.colour && m1.id == m2.id)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public static bool operator !=(Marker m1, Marker m2)
		{
			if (m1.colour != m2.colour || m1.id != m2.id)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
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
					markers.Add(new Marker(earth.transform.rotation, 1, instance));
				}
				else if (cmd.Length == 3)
				{
					switch (cmd[1])
					{
					case "red":
						instance.renderer.material.color = new Color(1, 0, 0);
						break;
					case "green":
						instance.renderer.material.color = new Color(0, 1, 0);
						break;
					case "blue":
						instance.renderer.material.color = new Color(0, 0, 1);
						break;
					case "yellow":
						instance.renderer.material.color = new Color(1, 1, 0);
						break;
					case "orange":
						instance.renderer.material.color = new Color(1, 0.5f, 0);
						break;
					case "purple":
						instance.renderer.material.color = new Color(1, 0, 1);
						break;
					case "pink":
						instance.renderer.material.color = new Color(1, 0, 0.5f);
						break;
					case "black":
						instance.renderer.material.color = new Color(0, 0, 0);
						break;
					case "white":
						instance.renderer.material.color = new Color(1, 1, 1);
						break;
					default:
						instance.renderer.material.color = new Color(1, 0, 0);
						break;
					}
					
					markers.Add(new Marker(earth.transform.rotation, 1, instance));
				}
			}
			else if (cmd[0] == "delete" || cmd[0] == "remove")
			{
				if (cmd[1] == "all" || cmd[1] == "everything")
				{
					if (cmd.Length == 2)
					{
						DeleteAll();
					}
					else if (cmd.Length == 3)
					{
						switch (cmd[2])
						{
						case "red":
							DeleteAll(new Color(1, 0, 0));
							break;
						case "green":
							DeleteAll(new Color(0, 1, 0));
							break;
						case "blue":
							DeleteAll(new Color(0, 0, 1));
							break;
						case "yellow":
							DeleteAll(new Color(1, 1, 0));
							break;
						case "orange":
							DeleteAll(new Color(1, 0.5f, 0));
							break;
						case "purple":
							DeleteAll(new Color(1, 0, 1));
							break;
						case "pink":
							DeleteAll(new Color(1, 0, 0.5f));
							break;
						case "black":
							DeleteAll(new Color(0, 0, 0));
							break;
						case "white":
							DeleteAll(new Color(1, 1, 1));
							break;
						}
					}
				}
				else
				{
					if (cmd.Length == 2)
					{
						switch (cmd[1])
						{
						case "red":
							DeleteSpecific(new Color(1, 0, 0));
							break;
						case "green":
							DeleteSpecific(new Color(0, 1, 0));
							break;
						case "blue":
							DeleteSpecific(new Color(0, 0, 1));
							break;
						case "yellow":
							DeleteSpecific(new Color(1, 1, 0));
							break;
						case "orange":
							DeleteSpecific(new Color(1, 0.5f, 0));
							break;
						case "purple":
							DeleteSpecific(new Color(1, 0, 1));
							break;
						case "pink":
							DeleteSpecific(new Color(1, 0, 0.5f));
							break;
						case "black":
							DeleteSpecific(new Color(0, 0, 0));
							break;
						case "white":
							DeleteSpecific(new Color(1, 1, 1));
							break;
						case "one":
							DeleteSpecific(1);
							break;
						case "two":
							DeleteSpecific(2);
							break;
						case "three":
							DeleteSpecific(3);
							break;
						case "four":
							DeleteSpecific(4);
							break;
						case "five":
							DeleteSpecific(5);
							break;
						case "six":
							DeleteSpecific(6);
							break;
						case "seven":
							DeleteSpecific(7);
							break;
						case "eight":
							DeleteSpecific(8);
							break;
						case "nine":
							DeleteSpecific(9);
							break;
						case "ten":
							DeleteSpecific(10);
							break;
						}
					}
					else if (cmd.Length == 3)
					{
						int id = 1;
						Color colour = new Color(1, 0, 0);

						switch (cmd[1])
						{
						case "red":
							colour = new Color(1, 0, 0);
							break;
						case "green":
							colour = new Color(0, 1, 0);
							break;
						case "blue":
							colour = new Color(0, 0, 1);
							break;
						case "yellow":
							colour = new Color(1, 1, 0);
							break;
						case "orange":
							colour = new Color(1, 0.5f, 0);
							break;
						case "purple":
							colour = new Color(1, 0, 1);
							break;
						case "pink":
							colour = new Color(1, 0, 0.5f);
							break;
						case "black":
							colour = new Color(0, 0, 0);
							break;
						case "white":
							colour = new Color(1, 1, 1);
							break;
						}

						switch (cmd[2])
						{
						case "one":
							id = 1;
							break;
						case "two":
							id = 2;
							break;
						case "three":
							id = 3;
							break;
						case "four":
							id = 4;
							break;
						case "five":
							id = 5;
							break;
						case "six":
							id = 6;
							break;
						case "seven":
							id = 7;
							break;
						case "eight":
							id = 8;
							break;
						case "nine":
							id = 9;
							break;
						case "ten":
							id = 10;
							break;
						}

						DeleteSpecific(colour, id);
					}
				}
			}
			else if (cmd[0] == "go" && cmd[1] == "to")
			{

			}
		}
	}

	void DeleteSpecific(Color colour)
	{
		List<Marker> results = markers.FindAll(
			delegate(Marker obj)
			{
				return obj.colour == colour;
			}
		);
		if (results.Count == 1)
		{
			Destroy(results[0].instance);
			markers.Remove(results[0]);
		}
		else
		{
			Debug.Log("No / More than one marker matches that desciption.");
		}
	}

	void DeleteSpecific(int id)
	{
		List<Marker> results = markers.FindAll(
			delegate(Marker obj)
			{
				return obj.id == id;
			}
		);
		if (results.Count == 1)
		{
			Destroy(results[0].instance);
			markers.Remove(results[0]);
		}
		else
		{
			Debug.Log("No / More than one marker matches that desciption.");
		}
	}

	void DeleteSpecific(Color colour, int id)
	{
		List<Marker> results = markers.FindAll(
			delegate(Marker obj)
			{
				return obj.id == id && obj.colour == colour;
			}
		);
		if (results.Count == 1)
		{
			Destroy(results[0].instance);
			markers.Remove(results[0]);
		}
		else
		{
			Debug.Log("No / More than one marker matches that desciption.");
		}
	}

	void DeleteAll()
	{
		foreach(Marker marker in markers)
		{
			Destroy(marker.instance);
		}
		markers.Clear();
	}

	void DeleteAll(Color colour)
	{
		foreach(Marker marker in markers)
		{
			if (marker.colour == colour)
			{
				Destroy(marker.instance);
				markers.Remove(marker);
			}
		}
	}
}
