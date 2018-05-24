using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class control : MonoBehaviour {
    public float trav=0;
    public int speed;
    public int turn2,turn,minSpeed;
    public float drift ;
    private int torque;
    private Rigidbody2D rb;
    private Vector2 distance;
    public Button left, right, br, ac;
    void Start () {
		rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
    }

    void FixedUpdate () {

        if (Input.GetAxis("Vertical") >= 0  )
        {
            torque = turn;
        }
        else torque = turn2;

        rb.AddForce(-Input.GetAxis("Vertical") * speed * transform.up);
        
        Vector2 forwardVel = transform.up * Vector2.Dot(rb.velocity,transform.up);
        Vector2 rightVel = transform.right * Vector2.Dot(rb.velocity, transform.right);
        rb.velocity = forwardVel + rightVel * drift;
        if(Mathf.Abs(forwardVel.y) > minSpeed || Mathf.Abs(forwardVel.x) > minSpeed)
        rb.angularVelocity = -Input.GetAxis("Horizontal") * torque;


        // Calculate Distance
        
        distance = Time.deltaTime * new Vector2(Mathf.Abs(rb.velocity.x),Mathf.Abs(rb.velocity.y));
        trav += distance.magnitude;
            

    }

    /// <summary>
    /// Calculate the distance the car travelled
    /// </summary>

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            Application.LoadLevel("Meniu");
        }
    }

    void CalculateDistanceAndAdd()
    {
        float timeInHours = (Time.smoothDeltaTime / 60) / 60;
        
    }

}
