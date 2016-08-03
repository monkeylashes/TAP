using UnityEngine;
using System.Collections.Generic;

public class ArrowPool : MonoBehaviour
{
    public GameObject ArrowPrefab;

    int maxAttackArrows = 2;
    List<GameObject> attackArrowPool;
    Arrow teleportArrow;
    Transform poolLocation;

	// Use this for initialization
	void Start ()
    {
	    for(int i = 0; i < maxAttackArrows; i++)
        {
            GameObject arrowTemp = Instantiate(ArrowPrefab);
            attackArrowPool.Add(arrowTemp);
        }
        //teleportArrow = Instantiate(
        //    )
	}
	
    public void ReturnToPool(GameObject arrow)
    {

    }

}
