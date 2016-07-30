using UnityEngine;
using System.Collections;

public class EyeScript : MonoBehaviour {

    public delegate void OnPlayerDetectedDelegate(GameObject gameObject);
    public OnPlayerDetectedDelegate subscribers;

    // Use this for initialization
    void Start () {
	    
	}
        
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collided with " + collider.gameObject.tag);
        if (subscribers != null)
        {
            subscribers(collider.gameObject);
        }
    }
}
