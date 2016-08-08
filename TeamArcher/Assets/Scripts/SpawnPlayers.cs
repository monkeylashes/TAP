using UnityEngine;
using BeardedManStudios.Network;
using System.Collections;

public class SpawnPlayers : MonoBehaviour {

    public GameObject objectToSpawn;
	// Use this for initialization
	void Start () {
        if (Networking.PrimarySocket.Connected)
        {
            Networking.Instantiate(objectToSpawn, NetworkReceivers.AllBuffered);
        }
        else
        {
            Networking.PrimarySocket.connected += delegate ()
            {
                Networking.Instantiate(objectToSpawn, NetworkReceivers.AllBuffered);
            };
        }
	}	
}
