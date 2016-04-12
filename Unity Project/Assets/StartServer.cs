using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class StartServer : MonoBehaviour {

	static Process server;

	// Use this for initialization
	void Start () {
		server = Process.Start (Application.dataPath + "/RecoServeur.exe");
	}

	void OnApplicationQuit() {
		server.Close ();
	}
}
