using UnityEngine;
using System.Collections;

public class LockRotation : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
