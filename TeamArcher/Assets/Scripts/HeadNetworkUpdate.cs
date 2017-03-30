using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class HeadNetworkUpdate : HeadBehavior
{

    protected GameObject controller;

    // Use this for initialization
    void Start()
    {
        GameObject player = GameObject.Find("[CameraRig]");
        this.controller = player.transform.FindChild("Camera (eye)").gameObject;
        if (networkObject.IsOwner)
        {
            transform.GetComponent<MeshRenderer>().enabled = false;
            //transform.GetComponent<Rigidbody>().isKinematic = true;
        }
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
