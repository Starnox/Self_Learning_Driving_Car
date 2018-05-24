using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTravelled : MonoBehaviour {

    private Vector2 lastPosition;
    private Vector2 currentPositon;
    private float distanceTravelled;

	
	void Start () {
        lastPosition = transform.position;
        distanceTravelled = 0;
	}
	
	
	void Update () {
        currentPositon = transform.position;
        if (currentPositon.y > lastPosition.y)
            distanceTravelled += Vector2.Distance(currentPositon, lastPosition);
        else
            distanceTravelled -= Vector2.Distance(currentPositon, lastPosition);
        lastPosition = currentPositon;
        Debug.Log("Distance: " + distanceTravelled);
	}
}
