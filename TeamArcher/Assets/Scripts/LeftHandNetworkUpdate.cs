using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class LeftHandNetworkUpdate : LeftHandBehavior
{

    protected GameObject controller;

    // Use this for initialization
    void Start () {
        GameObject player = GameObject.Find("[CameraRig]");
        this.controller = player.transform.FindChild("Controller (left)").gameObject;     
    }

    // Update is called once per frame
    void Update()
    {

        if (controller == null) { return; }

        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            return;
        }

        transform.position = controller.transform.position;
        transform.rotation = controller.transform.rotation;

        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;

    }
}
