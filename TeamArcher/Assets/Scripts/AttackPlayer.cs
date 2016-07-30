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
    public GameObject basicArrow;
    private GameObject arrow;
    private GameObject eyes;
    private GameObject enemy;
    
    // Use this for initialization
    void Start () {
        currentTarget = transform.position;
        currentState = State.Wonder;        
        eyes = transform.GetChild(0).gameObject;
        eyes.GetComponent<EyeScript>().subscribers += SawEnemy;
        arrow = basicArrow.transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        
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
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
            transform.LookAt(currentTarget);

            if(Vector3.Distance(transform.position, currentTarget) < 6.0f)
            {
                currentState = State.Attack;
            }
        }

        if(currentState == State.Attack)
        {
            currentTarget = enemy.transform.position;
            StartCoroutine(FireArrow());

            if (Vector3.Distance(transform.position, currentTarget) > 6.0f)
            {
                currentState = State.Wonder;
            }
        }        
	}

    IEnumerator FireArrow()
    {
        Vector3 relativePos = currentTarget - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);        
        
                
        if(arrow == null)
        {
            arrow = (GameObject)Instantiate(Resources.Load("Prefabs/BasicArrow"), transform.position, rotation);
            arrow.transform.parent = basicArrow.transform;            
            basicArrow.GetComponent<Rigidbody>().AddForce(Random.Range(8f, 15.0f) * 1.5f * arrow.transform.TransformDirection(Vector3.forward), ForceMode.Impulse);
            basicArrow.GetComponent<Rigidbody>().isKinematic = false;
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            arrow.GetComponent<Arrow>().inFlight = true;
        }        

        if (arrow.GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            arrow.GetComponent<Arrow>().inFlight = false;
            arrow.GetComponent<Arrow>().collided = true;
            arrow.GetComponent<Arrow>().DestroyArrow(1f);

            arrow = null;
            yield return new WaitForSeconds(1);
        }       
        
    }

    void SawEnemy(GameObject enemy)
    {
        // Evaluate what we see    
        if(enemy.CompareTag("Player"))
        {
            Debug.Log("Seen Player!");
            currentTarget = enemy.transform.position;
            currentState = State.Chase;
            this.enemy = enemy;
        }


    }
}
