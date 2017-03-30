using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;

public class NetworkSpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        NetworkManager.Instance.InstantiateBasicArrow();
        NetworkManager.Instance.InstantiateTeleportArrow();
        NetworkManager.Instance.InstantiateBasicBow();

        NetworkManager.Instance.InstantiateHead();
        NetworkManager.Instance.InstantiateLeftHand();
        NetworkManager.Instance.InstantiateRightHand();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
