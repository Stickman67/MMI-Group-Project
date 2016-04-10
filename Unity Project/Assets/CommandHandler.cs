using UnityEngine;
using UnityEngine.UI;
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
	private SimpleGestureListener GestureListener;

	// Use this for initialization
	void Start () {
		commandQueue = new Queue<string> ();
		markers = new List<Marker> ();
		GestureListener = Camera.main.GetComponent<SimpleGestureListener>();
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
					PlaceMarker(instance, Color.red);
				}
				else if (cmd.Length == 3)
				{
					switch (cmd[1])
					{
					case "red":
						PlaceMarker(instance, Color.red);
						break;
					case "green":
						PlaceMarker(instance, Color.green);
						break;
					case "blue":
						PlaceMarker(instance, Color.blue);
						break;
					case "yellow":
						PlaceMarker(instance, Color.yellow);
						break;
					case "orange":
						PlaceMarker(instance, new Color(1, 0.5f, 0));
						break;
					case "purple":
						PlaceMarker(instance, new Color(1, 0, 1));
						break;
					case "pink":
						PlaceMarker(instance, new Color(1, 0, 0.5f));
						break;
					case "black":
						PlaceMarker(instance, Color.black);
						break;
					case "white":
						PlaceMarker(instance, Color.white);
						break;
					default:
						PlaceMarker(instance, Color.red);
						break;
					}
				}
			}
			else if ((cmd[0] == "delete" || cmd[0] == "remove") && GestureListener.waving)
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
							DeleteAll(Color.red);
							break;
						case "green":
							DeleteAll(Color.green);
							break;
						case "blue":
							DeleteAll(Color.blue);
							break;
						case "yellow":
							DeleteAll(Color.yellow);
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
							DeleteAll(Color.black);
							break;
						case "white":
							DeleteAll(Color.white);
							break;
						}

						if (markers.Count != 0)
							markers.Sort((a, b) => a.id.CompareTo(b.id));
					}
				}
				else
				{
					if (cmd.Length == 2)
					{
						switch (cmd[1])
						{
						case "red":
							DeleteSpecific(Color.red);
							break;
						case "green":
							DeleteSpecific(Color.green);
							break;
						case "blue":
							DeleteSpecific(Color.blue);
							break;
						case "yellow":
							DeleteSpecific(Color.yellow);
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
							DeleteSpecific(Color.black);
							break;
						case "white":
							DeleteSpecific(Color.white);
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
						
						if (markers.Count != 0)
							markers.Sort((a, b) => a.id.CompareTo(b.id));
					}
					else if (cmd.Length == 3)
					{
						int id = 1;
						Color colour = Color.red;

						switch (cmd[1])
						{
						case "red":
							colour = Color.red;
							break;
						case "green":
							colour = Color.green;
							break;
						case "blue":
							colour = Color.blue;
							break;
						case "yellow":
							colour = Color.yellow;
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
							colour = Color.black;
							break;
						case "white":
							colour = Color.white;
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
						
						if (markers.Count != 0)
							markers.Sort((a, b) => a.id.CompareTo(b.id));
					}
				}
			}
			else if (cmd[0] == "go" && cmd[1] == "to")
			{
				if (cmd.Length == 3)
				{
					switch (cmd[2])
					{
					case "red":
						Goto(Color.red);
						break;
					case "green":
						Goto(Color.green);
						break;
					case "blue":
						Goto(Color.blue);
						break;
					case "yellow":
						Goto(Color.yellow);
						break;
					case "orange":
						Goto(new Color(1, 0.5f, 0));
						break;
					case "purple":
						Goto(new Color(1, 0, 1));
						break;
					case "pink":
						Goto(new Color(1, 0, 0.5f));
						break;
					case "black":
						Goto(Color.black);
						break;
					case "white":
						Goto(Color.white);
						break;
					case "one":
						Goto(1);
						break;
					case "two":
						Goto(2);
						break;
					case "three":
						Goto(3);
						break;
					case "four":
						Goto(4);
						break;
					case "five":
						Goto(5);
						break;
					case "six":
						Goto(6);
						break;
					case "seven":
						Goto(7);
						break;
					case "eight":
						Goto(8);
						break;
					case "nine":
						Goto(9);
						break;
					case "ten":
						Goto(10);
						break;
					}
				}
				else if (cmd.Length == 4)
				{
					int id = 1;
					Color colour = new Color(1, 0, 0);
					
					switch (cmd[2])
					{
					case "red":
						colour = Color.red;
						break;
					case "green":
						colour = Color.green;
						break;
					case "blue":
						colour = Color.blue;
						break;
					case "yellow":
						colour = Color.yellow;
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
						colour = Color.black;
						break;
					case "white":
						colour = Color.white;
						break;
					}
					
					switch (cmd[3])
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
					
					Goto(colour, id);
				}
			}
		}
	}

	void PlaceMarker(GameObject instance, Color colour)
	{
		instance.renderer.material.color = colour;
		int id = GetNextInt(colour);
		if (id < 10)
		{
			instance.transform.GetChild(0).GetChild(0).GetComponent<Text> ().text = IntToWord(id);
			markers.Add(new Marker(earth.transform.rotation, id, instance));
			markers.Sort((a, b) => a.id.CompareTo(b.id));
		}
		else
		{
			Debug.Log("No more than 10 markers of one colour can be placed.");
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
			}
		}

		markers.RemoveAll (marker => marker.colour == colour);
	}

	void Goto(Color colour)
	{
		List<Marker> results = markers.FindAll (marker => marker.colour == colour);

		if (results.Count == 1)
		{
			earth.transform.rotation = results[0].earthRotation;
		}
		else
		{
			Debug.Log("No / More than one marker matches that desciption.");
		}
	}
	
	void Goto(int id)
	{
		List<Marker> results = markers.FindAll(marker => marker.id == id);

		if (results.Count == 1)
		{
			earth.transform.rotation = results[0].earthRotation;
		}
		else
		{
			Debug.Log("No / More than one marker matches that desciption.");
		}
	}

	void Goto(Color colour, int id)
	{
		
		List<Marker> results = markers.FindAll (marker => marker.id == id && marker.colour == colour);

		if (results.Count == 1)
		{
			earth.transform.rotation = results[0].earthRotation;
		}
		else
		{
			Debug.Log("No / More than one marker matches that desciption.");
		}
	}

	string IntToWord(int n)
	{
		switch (n) 
		{
		case 1:
			return "One";
		case 2:
			return "Two";
		case 3:
			return "Three";
		case 4:
			return "Four";
		case 5:
			return "Five";
		case 6:
			return "Six";
		case 7:
			return "Seven";
		case 8:
			return "Eight";
		case 9:
			return "Nine";
		case 10:
			return "Ten";
		default:
			return "ERROR";
		}
	}

	int GetNextInt(Color colour)
	{
		List<Marker> colouredMarkers = new List<Marker>();
		
		foreach (Marker marker in markers) {
			if (marker.colour == colour) {
				colouredMarkers.Add (marker);
			}
		}
		
		colouredMarkers.Sort((a, b) => a.id.CompareTo(b.id));

		if (colouredMarkers.Count == 0) {
			return 1;
		}
		else
		{
			int counter = 1;

			while (counter < 10)
			{
				foreach(Marker marker in colouredMarkers)
				{
					if (counter != marker.id)
					{
						return counter;
					}

					counter++;
				}
			};
			return counter;
		}
	}
}
