using UnityEngine;
using System.Collections;

public class AttackPlayer : MonoBehaviour {

    private enum State
    {
        Wonder,
        Chase,
        Attack
    }

    State currentState = State.Wonder;
    Vector3 currentTarget = Vector3.zero;
    float speed = 4f;
    GameObject player;
    public GameObject arrow;

    // Use this for initialization
    void Start () {
        currentTarget = transform.position;
        currentState = State.Chase;
        player = GameObject.FindGameObjectWithTag("Player");        
    }
	
	// Update is called once per frame
	void Update () {
        
                // Evaluate what we see
        var eyes = transform.GetChild(0);
        
        if (currentState == State.Wonder)
        {
            if (transform.position == currentTarget)
            {
                // pick a different location
                currentTarget = new Vector3(Random.Range(0.0f, 10.0f), 0.5f, Random.Range(0.0f, 10.0f));
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
                transform.LookAt(currentTarget);
            }
        }

        if(currentState == State.Chase)
        {                   
            currentTarget = player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
            transform.LookAt(currentTarget);

            if(Vector3.Distance(transform.position, currentTarget) < 6.0f)
            {
                currentState = State.Attack;
            }
        }

        if(currentState == State.Attack)
        {
            arrow = (GameObject)Instantiate(Resources.Load("BasicArrow"), transform.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            arrow.GetComponent<Rigidbody>().velocity = 3.0f * 1.5f * arrow.transform.TransformDirection(Vector3.forward);
            arrow.GetComponent<Arrow>().inFlight = true;

            if (Vector3.Distance(transform.position, currentTarget) > 6.0f)
            {
                currentState = State.Wonder;
            }
        }        
	}
}
