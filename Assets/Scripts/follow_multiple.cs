using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_multiple : MonoBehaviour {
    
    public GameObject car1, car2;
    Camera cam;
    public float minZoom, maxZoom,zoomSpeed;
    Vector3  s;
	void Start () {
        cam  = this.GetComponent<Camera>();
        minZoom = 30f;
        maxZoom = 200f;
        zoomSpeed = 100f;
	}
	
	void Update () {
        Bounds b = new Bounds(car1.transform.position,Vector3.zero);
        b.Encapsulate(car1.transform.position);
        b.Encapsulate(car2.transform.position);
        //transform.position = Vector3.SmoothDamp(transform.position, b.center + new Vector3(0,0,-10),ref s,Time.deltaTime);
        transform.position = b.center + new Vector3(0, 0, -10);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Mathf.Lerp(minZoom, maxZoom, Mathf.Max(  b.size.x,b.size.y)/zoomSpeed),Time.deltaTime);
    }
}
