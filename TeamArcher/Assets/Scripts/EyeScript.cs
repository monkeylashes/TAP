using UnityEngine;
using System.Collections;

public class EyeScript : MonoBehaviour {

    public delegate void OnPlayerDetectedDelegate(GameObject gameObject);
    
    public OnPlayerDetectedDelegate onPlayerSeen;

    public OnPlayerDetectedDelegate onLoseSightOfPlayer;

    // Use this for initialization
    void Start () {
	    
	}

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("saw" + collider.gameObject.tag);
        if (onPlayerSeen != null)
        {
            onPlayerSeen(collider.gameObject);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("not saw" + collider.gameObject.tag);
        if (onLoseSightOfPlayer != null)
        {
            onLoseSightOfPlayer(collider.gameObject);
        }
    }

    //void OnTriggerStay(Collider collider)
    //{
    //    Debug.Log("still seeing" + collider.gameObject.tag);
    //    if (subscribers != null)
    //    {
    //        subscribers(collider.gameObject);
    //    }

    //}
}
