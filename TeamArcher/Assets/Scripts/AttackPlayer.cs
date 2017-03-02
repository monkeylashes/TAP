using UnityEngine;
using System.Collections;
using VRTK.Examples.Archery;

public class AttackPlayer : MonoBehaviour {

    [HideInInspector]
    public enum State

    {
        Wonder,
        Chase,
        Attack,
        die,
        hurt
    }

    [HideInInspector]
    public State currentState = State.Wonder;

    State previousState = State.Wonder;
    Vector3 currentTarget = Vector3.zero;
    public float speed = 4f;

    private GameObject spit;
    public GameObject spitPrefab;

    private GameObject eyes;
    private GameObject enemy;
    public Animation animation;
    
    // Use this for initialization
    void Start () {
        currentTarget = transform.position;
        currentState = State.Wonder;        
        eyes = transform.GetChild(0).gameObject;
        eyes.GetComponent<EyeScript>().onPlayerSeen += SawEnemy;
        eyes.GetComponent<EyeScript>().onLoseSightOfPlayer += EnemyLost;   
    }

    void UpdateAnimation()
    {
        if(currentState != previousState)
        {
            if(currentState == State.Attack)
            {
                if(Random.value > 0.5f)
                {
                    animation.Play("Spider_attack_A");
                }
                else
                {
                    animation.Play("Spider_attack_B");
                }

                previousState = State.Attack;
            }

            if(currentState == State.Wonder)
            {
                speed = 2f;
                animation.Play("Spider_walk");
                previousState = State.Wonder;

            }

            if (currentState == State.Chase)
            {
                speed = 4f;
                animation.Play("Spider_run");
                previousState = State.Chase;
            }

            if(currentState == State.die)
            {
                speed = 0.0f;
                if (Random.value > 0.5f)
                {
                    animation.Play("Spider_daying_B");
                }
                else
                {
                    animation.Play("Spider_daying_A");

                }
                previousState = State.die;
                DestroyObject(transform.gameObject, 2f);
            }

            if(currentState == State.hurt)
            {
                speed = 1.0f;
                if (Random.value > 0.5f)
                {
                    animation.Play("Spider_defend_A");
                }
                else
                {
                    animation.Play("Spider_defend_B");
                }
                previousState = State.hurt;
                currentState = State.Wonder;
            }


        }
    }
	
	// Update is called once per frame
	void Update () {
        
        if (animation)
        {
            UpdateAnimation();

        }

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
	}

    float lastFireTime = 0f;
    
    void FixedUpdate()
    {
        if (currentState == State.Attack && enemy != null)
        {                                   
            if(Vector3.Distance(transform.position, currentTarget) < 1f)
            {
                // hit player if close enough to reach
                currentTarget = enemy.transform.position;
                Vector3 relativePos = currentTarget - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                enemy.GetComponent<Rigidbody>().AddForce(Random.Range(.3f, 1f) * transform.forward, ForceMode.Impulse);
            }
            else
            {
                // spit at player
                if (Time.fixedTime - lastFireTime > 1.333f)
                {
                    Vector3 relativePos = currentTarget - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(relativePos);

                    spit = Instantiate(spitPrefab, transform.position, rotation);                    
                    lastFireTime = Time.fixedTime;
                    spit.GetComponent<Rigidbody>().AddForce(Random.Range(15.0f, 40.0f) * 1.5f * spit.transform.TransformDirection(Vector3.forward), ForceMode.Impulse);
                    spit.GetComponent<Rigidbody>().isKinematic = false;
                    Destroy(spit, 1f);
                }
            }


            if (Vector3.Distance(transform.position, currentTarget) > 6.0f)
            {
                currentState = State.Wonder;
            }
        }
    }    

    void SawEnemy(GameObject enemy)
    {
        // Evaluate what we see    
        if(enemy.CompareTag("Shootable"))
        {
            //Debug.Log("Seen Player!");
            currentTarget = enemy.transform.position;
            currentState = State.Chase;
            this.enemy = enemy;
        }
    }

    void EnemyLost(GameObject enemy)
    {
        // Evaluate what we see    
        if (enemy.CompareTag("Shootable"))
        {
            //Debug.Log("lost enemy!");
            currentTarget = enemy.transform.position;
            currentState = State.Wonder;
            this.enemy = null;
        }


    }
}
