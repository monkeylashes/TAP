using UnityEngine;
using System.Collections;

public class FPSControllerNOVR : MonoBehaviour
{
    public bool isOn = true;
    public float mouseSensitivity = 10.0f;
    public GameObject arrowPrefab;

    void Awake()
    {
        if (false == isOn)
        {
            this.enabled = false;
            return;
        }

        gameObject.transform.position = gameObject.transform.position + Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if (Input.mousePosition.x > Screen.width)
            {
                gameObject.transform.Rotate(Vector3.up, Time.deltaTime * mouseSensitivity);
            }
            if (Input.mousePosition.x < 0)
            {
                gameObject.transform.Rotate(Vector3.down, Time.deltaTime * mouseSensitivity);
            }
            if (Input.mousePosition.y > Screen.height)
            {
                gameObject.transform.Rotate(Vector3.left, Time.deltaTime * mouseSensitivity);
            }
            if (Input.mousePosition.y < 0)
            {
                gameObject.transform.Rotate(Vector3.right, Time.deltaTime * mouseSensitivity);
            }

            if (Input.GetMouseButtonDown(0))
            {
                //fire arrow
                FireArrow();
            }
            if (Input.GetMouseButtonDown(1))
            {
                //fire position arrow
                WhatsThat();
            }



        }


    }

    void FireArrow()
    {
        Ray arrowLine = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        GameObject firingArrow = (GameObject)Instantiate(arrowPrefab, arrowLine.origin, Quaternion.FromToRotation(Vector3.up, arrowLine.direction));
        Debug.Log(arrowLine);
        //Vector3 relativePos = currentTarget - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos);

        firingArrow.GetComponent<Rigidbody>().AddForce(Random.Range(15.0f, 20.0f) * 1.5f * arrowLine.direction, ForceMode.Impulse);
        firingArrow.GetComponent<Rigidbody>().isKinematic = false;

        firingArrow.GetComponent<Arrow>().inFlight = true;
        //if (arrow == null)
        //{
        //    DestroyObject(basicArrow, 1f);

        //    basicArrow = (GameObject)Instantiate(Resources.Load("Prefabs/BasicArrow"), transform.position, rotation);
        //    arrow = basicArrow.transform.GetChild(0).gameObject;

        //    basicArrow.GetComponent<Rigidbody>().AddForce(Random.Range(15.0f, 20.0f) * 1.5f * basicArrow.transform.TransformDirection(Vector3.forward), ForceMode.Impulse);
        //    basicArrow.GetComponent<Rigidbody>().isKinematic = false;

        //    arrow.GetComponent<Arrow>().inFlight = true;
        //}

        //if (basicArrow.GetComponent<Rigidbody>().velocity == Vector3.zero)
        //{
        //    arrow.GetComponent<Arrow>().inFlight = false;
        //    arrow.GetComponent<Arrow>().collided = true;
        //    arrow.GetComponent<Arrow>().DestroyArrow(1f);

        //    arrow = null;
        //    yield return new WaitForSeconds(5);
        //}

    }

    void WhatsThat()
    {
        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Debug.Log(Input.mousePosition);
        Debug.Log(ray);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            print("I'm looking at " + hit.transform.name);
        else
            print("I'm looking at nothing!");
    }
}
