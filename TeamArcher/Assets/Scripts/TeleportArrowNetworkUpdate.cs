using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
public class TeleportArrowNetworkUpdate : TeleportArrowBehavior {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(networkObject == null)
        {
            return;
        }
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            return;
        }

        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
    }
}
