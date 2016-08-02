using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    private Vector3 targetPosition;

    public float speed = 5f;
    public float startTime;
    private float journeyLength;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        targetPosition = transform.position;
        journeyLength = Vector3.Distance(transform.position, targetPosition);
    }
	
	// Update is called once per frame
	void Update () {
	    if(targetPosition != transform.position)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
        }
	}

    public void SetTargetPosition(Vector3 pos)
    {
        startTime = Time.time;
        targetPosition = pos;
        journeyLength = Vector3.Distance(transform.position, targetPosition);
    }
}
