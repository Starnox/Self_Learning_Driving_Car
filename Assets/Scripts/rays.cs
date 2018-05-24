using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rays : MonoBehaviour {

    public Text t1, t2, t3;
    public float  d1, d2, d3;
    private RaycastHit2D ray1, ray2, ray3;
    private LineRenderer line1, line2, line3;

	void Start () {
        line1 = (new GameObject()).AddComponent<LineRenderer>();
        line2 = (new GameObject()).AddComponent<LineRenderer>();
        line3 = (new GameObject()).AddComponent<LineRenderer>();
        line1.SetWidth(0.5f,0.5f);
        line2.SetWidth(0.5f, 0.5f);
        line3.SetWidth(0.5f, 0.5f);
        line1.material = new Material(Shader.Find("Particles/Additive"));
        line2.material = new Material(Shader.Find("Particles/Additive"));
        line3.material = new Material(Shader.Find("Particles/Additive"));
        line1.SetColors(Color.green, Color.green);
        line2.SetColors(Color.green, Color.green);
        line3.SetColors(Color.green, Color.green);
    }
	
	// Update is called once per frame
	void Update () {
        t1.text = d1.ToString("0.00");
        t2.text = d2.ToString("0.00");
        t3.text = d3.ToString("0.00");

        ray1 = Physics2D.Raycast(transform.position - 6* transform.up, -transform.up, 25f, ~((1<<9)|(1<<10)));

        ray2 = Physics2D.Raycast(transform.position - 6 * transform.up, -transform.up - 3 * transform.right,20f, ~((1 << 9) | (1 << 10)));
        
        ray3 = Physics2D.Raycast(transform.position - 6 * transform.up, -transform.up + 3 * transform.right,20f, ~((1 << 9) | (1 << 10)));
        
        d1 = ray1.distance;
        d2 = ray2.distance;
        d3 = ray3.distance;

        if (d1 == 0)
            d1 = -1;

        if (d2 == 0)
            d2 = -1;

        if (d3 == 0)
            d3 = -1;

        if (d1 != -1)
        {
            line1.enabled = true;
            line1.SetPosition(0, transform.position - 6 * transform.up);
            line1.SetPosition(1, new Vector3(ray1.point.x, ray1.point.y,-2));
            Debug.Log(ray1.collider);
        }
        else line1.enabled = false;

        if (d2 != -1)
        {
            line2.enabled = true;
            line2.SetPosition(0, transform.position - 6 * transform.up);
            line2.SetPosition(1, new Vector3(ray2.point.x, ray2.point.y, -2));
        }
        else line2.enabled = false;

        if (d3 != -1)
        {
            line3.enabled = true;
            line3.SetPosition(0, transform.position - 6 * transform.up);
            line3.SetPosition(1, new Vector3(ray3.point.x, ray3.point.y, -2));
        }
        else line3.enabled = false;

    }

    private void FixedUpdate()
    {
        
    }
}
