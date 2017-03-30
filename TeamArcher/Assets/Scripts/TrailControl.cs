using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples.Archery;

public class TrailControl : MonoBehaviour {

    private Arrow arrow;
    private TrailRenderer trailRenderer;

	// Use this for initialization
	void Start () {        
        arrow = gameObject.GetComponentInParent<Arrow>();
        trailRenderer = transform.GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (arrow.inFlight)
        {
            trailRenderer.enabled = true;
        }
        else
        {
            trailRenderer.enabled = false;
        }
	}
}
