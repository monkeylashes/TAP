using UnityEngine;
using System.Collections;

public class TeleportArrow : Arrow {

    public GameObject player = null;    

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Teleport arrow collided!");
        if (player == null)
        {
            //Debug.Log("Player was null. Setting with tag.");
            player = GameObject.FindGameObjectWithTag("Player");

        }
        var pos = collision.contacts[0].point;
        //Debug.Log("player position: " + player.transform.position + " teleporting to: " + pos);
        player.transform.position = pos;
                
        if (inFlight)
        {
            ResetArrow();
        }
    }
}
