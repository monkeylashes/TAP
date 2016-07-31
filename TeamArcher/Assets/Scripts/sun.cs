using UnityEngine;
using System.Collections;

public class sun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.left * (.5f * Time.deltaTime));
	}
}
