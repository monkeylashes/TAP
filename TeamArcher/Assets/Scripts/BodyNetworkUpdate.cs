using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class BodyNetworkUpdate : BodyBehavior {

    public float radius = 0.25f;
    public float maxHeight = 2f;
    public float minHeight = 0.3f;
    private float height = 2f;
    private GameObject bodyMesh;

    GameObject controller;
	// Use this for initialization
	void Start () {
        GameObject player = GameObject.Find("[CameraRig]");
        this.controller = player.transform.FindChild("Camera (eye)").gameObject;
        if (networkObject.IsOwner)
        {
            GetComponentInChildren<CapsuleCollider>().enabled = false;            
        }
        bodyMesh = transform.GetChild(0).gameObject;
        bodyMesh.GetComponent<MeshRenderer>().enabled = false;
    }

    void FixedUpdate()
    {       
        // caculate transforms
        RaycastHit rayCastHit;
        Ray ray = new Ray(controller.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out rayCastHit, maxHeight))
        {
            if (rayCastHit.distance > maxHeight)
            {
                height = maxHeight;
            }
            else
            {
                if(rayCastHit.distance >= minHeight)
                {
                    height = rayCastHit.distance;
                }                
            }
        }else
        {
            height = maxHeight;
        }
    }

    // Update is called once per frame
    void Update () {
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
        }

        Vector3 pos = new Vector3(controller.transform.position.x, controller.transform.position.y - (height / 2f), controller.transform.position.z);
        transform.position = pos;

        Quaternion rotation = controller.transform.rotation;
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = rotation;

        Vector3 scale = transform.transform.localScale;
        scale.y = height;
        transform.localScale = scale;

        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
        networkObject.scale = transform.localScale;          
	}
}
