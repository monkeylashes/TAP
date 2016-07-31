using UnityEngine;
using System.Collections;

public class TakeDamage : MonoBehaviour {
    int health = 60;
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collided");

        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Arrow"))
        {
            //Debug.Log("collided with arrow");
            collidedObject.GetComponent<Rigidbody>().isKinematic = true;
            collidedObject.transform.position = collision.contacts[0].point;
            collidedObject.transform.parent = transform;
            health -= 20;

            if(health <= 0)
            {
                health = 0;                
                transform.parent.gameObject.GetComponent<AttackPlayer>().currentState = AttackPlayer.State.die;
            }
            else
            {
                // play hurt animation
                transform.parent.gameObject.GetComponent<AttackPlayer>().currentState = AttackPlayer.State.hurt;
            }            
        }

    }
}
