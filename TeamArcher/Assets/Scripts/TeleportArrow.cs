using UnityEngine;
using System.Collections;
using VRTK.Examples.Archery;
public class TeleportArrow : Arrow
{

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
        player.GetComponent<Move>().SetTargetPosition(pos);
                
        if (inFlight)
        {
            ResetArrow();
        }
    }
}
