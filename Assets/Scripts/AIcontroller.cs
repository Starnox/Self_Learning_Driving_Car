using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIcontroller : MonoBehaviour {

    public Text  mij11, mij12, mij13, mij14, mij21, mij22, mij23, mij24, out1, out2;
    float xmij11, xmij12, xmij13, xmij14, xmij21, xmij22, xmij23, xmij24, xout1, xout2;
    private rays scr;
    float d1, d2, d3;
    public float[,,] a = new float[4, 5, 5];
    public Text curfit;
    Vector2 distance;
    public int speed;
    public int turn2, turn, minSpeed;
    public float drift,trav;
    private int torque;
    private Rigidbody2D rb;
    private genetic gen;
    public GameObject selection;
    private float lastime;
    public int maxTime;
    public Text time,value;
    public Slider maxt;
    void Start () {
        lastime = Time.time;
        gen = selection.GetComponent<genetic>();
        scr = GetComponent<rays>();
        rb = GetComponent<Rigidbody2D>();
        maxt.value = 30;
    }

    // Update is called once per frame
    void Update()
    {
        curfit.text = "Current fitness: " + trav.ToString("0.0000");
        d1 = scr.d1;
        d2 = scr.d2;
        d3 = scr.d3;
        retea();
        drive();
        afis();
        fitness();
    }

    void fitness()
    {
        distance = Time.deltaTime * new Vector2(Mathf.Abs(rb.velocity.x), Mathf.Abs(rb.velocity.y));
        trav += distance.magnitude;
    }

    void drive()
    {
        maxTime = (int)maxt.value;
        time.text = "Time Remaining: " + ((int) (maxTime - Time.time + lastime)).ToString();
        value.text = maxTime.ToString();
        if (Time.time-lastime > maxTime)
        {
            gen.col = true;
            gen.fitness = (trav * 3 + trav / (Time.time - lastime)) / 4;
            if (float.IsNaN(gen.fitness))
                gen.fitness = 1;
            trav = 0;
            lastime = Time.time;
            d1 = 0;
            d2 = 0;
            d3 = 0;
            distance = new Vector2(0, 0);
        }
        if(Mathf.Abs(rb.velocity.x) <= 0.5f && Mathf.Abs(rb.velocity.y) <= 0.5f)
            if(Time.time - lastime >= 3)
            {
                gen.col = true;
                gen.fitness = trav;
                if (float.IsNaN(gen.fitness))
                    gen.fitness = 1;
                trav = 0;
                lastime = Time.time;
                d1 = 0;
                d2 = 0;
                d3 = 0;
                distance = new Vector2(0, 0);
            }
       if (xout2 >= 0)
            torque = turn;
        else torque = turn2;

        rb.AddForce(-speed * xout2 * transform.up);

        Vector2 forwardVel = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVel = transform.right * Vector2.Dot(rb.velocity, transform.right);
        rb.velocity = forwardVel + rightVel * drift;
        if (Mathf.Abs(forwardVel.y) > minSpeed || Mathf.Abs(
            forwardVel.x) > minSpeed)
            rb.angularVelocity = -xout1 * torque;
    }

    void afis()
    {
        mij11.text = xmij11.ToString("0.00");
        mij12.text = xmij12.ToString("0.00");
        mij13.text = xmij13.ToString("0.00");
        mij14.text = xmij14.ToString("0.00");

        mij21.text = xmij21.ToString("0.00");
        mij22.text = xmij22.ToString("0.00");
        mij23.text = xmij23.ToString("0.00");
        mij24.text = xmij24.ToString("0.00");

        out1.text = xout1.ToString("0.00");
        out2.text = xout2.ToString("0.00");

    }

    void retea()
    {
        xmij11 = sigmoid(a[1, 1, 1] * d1 + a[1, 2, 1] * d2 + a[1, 3, 1] * d3);
        xmij12 = sigmoid(a[1, 1, 2] * d1 + a[1, 2, 2] * d2 + a[1, 3, 2] * d3);
        xmij13 = sigmoid(a[1, 1, 3] * d1 + a[1, 2, 3] * d2 + a[1, 3, 3] * d3);
        xmij14 = sigmoid(a[1, 1, 4] * d1 + a[1, 2, 4] * d2 + a[1, 3, 4] * d3);

        xmij21 = sigmoid((a[2, 1, 1] * xmij11 + a[2, 2, 1] * xmij12 + a[2, 3, 1] * xmij13 + a[2, 4, 1] * xmij14));
        xmij22 = sigmoid((a[2, 1, 2] * xmij11 + a[2, 2, 2] * xmij12 + a[2, 3, 2] * xmij13 + a[2, 4, 2] * xmij14));
        xmij23 = sigmoid((a[2, 1, 3] * xmij11 + a[2, 2, 3] * xmij12 + a[2, 3, 3] * xmij13 + a[2, 4, 3] * xmij14));
        xmij24 = sigmoid((a[2, 1, 4] * xmij11 + a[2, 2, 4] * xmij12 + a[2, 3, 4] * xmij13 + a[2, 4, 4] * xmij14));

        xout1 = sigmoid(a[3, 1, 1] * xmij21 + a[3, 2, 1] * xmij22 + a[3, 3, 1] * xmij23 + a[3, 4, 1] * xmij24);
        xout2 = sigmoid(a[3, 1, 2] * xmij21 + a[3, 2, 2] * xmij22 + a[3, 3, 2] * xmij23 + a[3, 4, 2] * xmij24);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (this.enabled == true)
        {
            gen.col = true;
            gen.fitness = (trav * 3 + trav / (Time.time - lastime)) / 4;
            if (float.IsNaN(gen.fitness))
                gen.fitness = 1;
            trav = 0;
            lastime = Time.time;
            d1 = 0;
            d2 = 0;
            d3 = 0;
            distance = new Vector2(0, 0);
        }
    }

    float sigmoid(float x)
    {
        return x / (1 + Mathf.Abs(x));
    }
}
