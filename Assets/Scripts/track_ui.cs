using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
public class track_ui : MonoBehaviour {

    public Button b1, b2,b3,b4,b5;
    public GameObject start, finish, r1, r2,r3;
    public Transform curent;
    float xoff, yoff, roff;
	class Road
    {
         public float x, y;
         public int type;
         public int rotation;
        public Road(float x_,float y_,int type_,int rot_)
        {
            x = x_;
            y = y_;
            type = type_;
            rotation = rot_;
        }
        public void rot()
        {
             rotation = (rotation + 1)%4;
        }
    };

    List<Road> roads = new List<Road>();
    List<GameObject> obj = new List<GameObject>();

	void Start () {
        string path = Application.persistentDataPath + "/Text.txt";
        string[] savefile = File.ReadAllLines(path);
        if (savefile[1][0] - '0' == 1)
        {
            GameObject st = Instantiate(start, curent.position, start.transform.rotation) as GameObject;
            roads.Add(new Road(0, 0, 0, 0));
            obj.Add(st);
            b1.onClick.AddListener(Type1);
            b2.onClick.AddListener(Type2);
            b3.onClick.AddListener(Type3);
            b4.onClick.AddListener(Type4);
            b5.onClick.AddListener(remove);
        }
        else
        {
            path = Application.persistentDataPath + savefile[0];
            string[] weights = File.ReadAllLines(path);
            float[] v = new float[4];
            for (int i = 0; i < weights.Length; i++)
            {

                int j = 0, nr, sign;
                for (int x = 0; x < 4; x++)
                {
                    nr = 0;
                    sign = 1;
                    
                    while (j < weights[i].Length && weights[i][j] != ' '  )
                    {
                        if (weights[i][j] == '-')
                        {
                            sign = -1;
                        }
                        if (weights[i][j] >= '0' && weights[i][j] <= '9')
                        {
                            nr = nr * 10 + weights[i][j] - '0';
                        }
                        j++;
                        Debug.Log(i + " " + j);
                    }
                    j++;
                    v[x] = (float)(nr * sign) / 100;
                }
                roads.Add(new Road(v[0], v[1], (int)v[2], (int)v[3]));
                 switch ((int)v[2])
                {
                    case 0:
                        {
                            GameObject r = Instantiate(start, new Vector3(v[0], v[1], curent.position.z), start.transform.rotation) as GameObject;
                            r.transform.Rotate(new Vector3(0, 0, v[3] * 90));
                            obj.Add(r);
                            break;
                        }
                    case 1:
                        {
                            GameObject r = Instantiate(r1, new Vector3(v[0], v[1], curent.position.z), r1.transform.rotation) as GameObject;
                            r.transform.Rotate(new Vector3(0, 0, v[3] * 90));
                            obj.Add(r);
                            break;
                        }
                    case 2:
                        {
                            GameObject r = Instantiate(r2, new Vector3(v[0], v[1], curent.position.z), r2.transform.rotation) as GameObject;
                            r.transform.Rotate(new Vector3(0, 0, v[3] * 90));
                            obj.Add(r);
                            break;
                        }
                    case 3:
                        {
                            GameObject r = Instantiate(r3, new Vector3(v[0], v[1], curent.position.z), r3.transform.rotation) as GameObject;
                            r.transform.Rotate(new Vector3(0, 0, v[3] * 90));
                            obj.Add(r);
                            break;
                        }
                    case 4:
                        {
                            GameObject r = Instantiate(finish, new Vector3(v[0], v[1], curent.position.z), finish.transform.rotation) as GameObject;
                            r.transform.Rotate(new Vector3(0, 0, v[3] * 90));
                            obj.Add(r);
                            break;
                        }
                }  
            }
            roads.RemoveAt(roads.Count - 1);
            Destroy(obj[obj.Count - 1]);
            obj.RemoveAt(obj.Count - 1);
            curent.position = new Vector3(roads[roads.Count - 1].x, roads[roads.Count - 1].y, -10);

            b1.onClick.AddListener(Type1);
            b2.onClick.AddListener(Type2);
            b3.onClick.AddListener(Type3);
            b4.onClick.AddListener(Type4);
            b5.onClick.AddListener(remove);
        }
    }
	
	void Update () {
        if (Input.GetKeyDown("backspace"))
            remove();
    }

    void Type1()
    {
        Debug.Log(roads[roads.Count - 1].type + " " + roads[roads.Count - 1].rotation);
        switch (roads[roads.Count - 1].type)
        {
            case 0:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                xoff = 0f;
                                yoff = 88.8f;
                                break;
                            }  
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                        GameObject r = Instantiate(r1, curent.position, r1.transform.rotation) as GameObject;
                        roads.Add(new Road(curent.position.x, curent.position.y, 1, 0));
                        obj.Add(r); 

                    break;
                }
            case 1:
                {
                    switch(roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                xoff = 0f;
                                yoff = 59f;
                                break;
                            }
                        case 1:
                            {
                                xoff = 59f;
                                yoff = 0f;
                                break;
                            }
                        case 2:
                            {
                                xoff = 0f;
                                yoff = -59f;
                                break;
                            }
                        case 3:
                            {
                                xoff = -59f;
                                yoff = 0;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    GameObject r = Instantiate(r1, curent.position, obj[obj.Count - 1].transform.rotation) as GameObject;
                        roads.Add(new Road(curent.position.x, curent.position.y, 1, roads[roads.Count - 1].rotation));
                        obj.Add(r);
                    break;
                }
            case 2:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                roff = -90;
                                xoff = -78.8f;
                                yoff = 12.8f;
                                break;
                            }
                        case 1:
                            {
                                roff = 0;
                                xoff = 12.8f;
                                yoff = 78.6f;
                                break;
                            }
                        case 2:
                            {
                                roff = 90;
                                xoff = 78.8f;
                                yoff = -12.8f;
                                break;
                            }
                        case 3:
                            {
                                roff = 180;
                                xoff = -12.8f;
                                yoff = -78.8f;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    //curent.Rotate(new Vector3(0, 0, roff));
                        GameObject r = Instantiate(r1, curent.position, r1.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, roff));
                        roads.Add(new Road(curent.position.x, curent.position.y, 1, (roads[roads.Count - 1].rotation + 3) % 4));
                        obj.Add(r);
                    break;
                }
            case 3:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                roff = +90;
                                xoff = +78.8f;
                                yoff = 12.8f;
                                break;
                            }
                        case 1:
                            {
                                roff = 180;
                                xoff = 12.8f;
                                yoff = -78.6f;
                                break;
                            }
                        case 2:
                            {
                                roff = -90;
                                xoff = -78.8f;
                                yoff = -12.8f;
                                break;
                            }
                        case 3:
                            {
                                roff = 0;
                                xoff = -12.8f;
                                yoff = +78.8f;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    //curent.Rotate(new Vector3(0, 0, roff));
                        GameObject r = Instantiate(r1, curent.position, r1.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, roff));
                        roads.Add(new Road(curent.position.x, curent.position.y, 1, (roads[roads.Count - 1].rotation + 1) % 4));
                        obj.Add(r);
                    break;
                }
        }

    }

    
    void Type2()
    {
        Debug.Log(roads[roads.Count - 1].type + " " + roads[roads.Count - 1].rotation);
        switch (roads[roads.Count - 1].type)
        {
            case 0:
                {
                    xoff = -12.6f;
                    yoff = 78.5f;
                    curent.position += new Vector3(xoff, yoff, 0);
                        GameObject r = Instantiate(r2, curent.position, r2.transform.rotation) as GameObject;
                        roads.Add(new Road(curent.position.x, curent.position.y, 2, 0));
                        obj.Add(r);
                    break;
                }
            case 1:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                roff = 0;
                                xoff = -12.6f;
                                yoff = 49f;
                                break;
                            }
                        case 1:
                            {
                                roff = 90;
                                xoff = 49f;
                                yoff = 12.6f;
                                break;
                            }
                        case 2:
                            {
                                roff = 180;
                                xoff = 12.7f;
                                yoff = -48.4f;
                                break;
                            }
                        case 3:
                            {
                                roff = -90;
                                xoff = -48.4f;
                                yoff = -12.6f;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    //curent.Rotate(new Vector3(0, 0, roff));
                        GameObject r = Instantiate(r2, curent.position, r2.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, roff));
                        roads.Add(new Road(curent.position.x, curent.position.y, 2, roads[roads.Count - 1].rotation));
                        obj.Add(r);
                        break;
                }
            case 2:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                roff = -90;
                                xoff = -68.3f;
                                yoff = 0;
                                break;
                            }
                        case 1:
                            {
                                roff = 0;
                                xoff = 0;
                                yoff = 68.3f;
                                break;
                            }
                        case 2:
                            {
                                roff = 90;
                                xoff = 68.3f;
                                yoff = 0f;
                                break;
                            }
                        case 3:
                            {
                                roff = -180;
                                xoff = 0f;
                                yoff = -68.3f;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    // curent.Rotate(new Vector3(0, 0, roff));
                        GameObject r = Instantiate(r2, curent.position, r2.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, roff));
                        roads.Add(new Road(curent.position.x, curent.position.y, 2, (roads[roads.Count - 1].rotation + 3) % 4));
                        obj.Add(r);
                    break;
                }
            case 3:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                roff = +90;
                                xoff = 68.3f;
                                yoff = 25.3f;
                                break;
                            }
                        case 1:
                            {
                                roff = 180;
                                xoff = 25.3f;
                                yoff = -68.3f;
                                break;
                            }
                        case 2:
                            {
                                roff = -90;
                                xoff = -68.3f;
                                yoff = -25.3f;
                                break;
                            }
                        case 3:
                            {
                                roff = 0;
                                xoff = -25.3f;
                                yoff = 68.3f;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    // curent.Rotate(new Vector3(0, 0, roff));
                        GameObject r = Instantiate(r2, curent.position, r2.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, roff));
                        roads.Add(new Road(curent.position.x, curent.position.y, 2, (roads[roads.Count - 1].rotation + 1) % 4));
                        obj.Add(r);
                    break;
                }
        }

    }
    void Type3()
    {
        Debug.Log(roads[roads.Count - 1].type + " " + roads[roads.Count - 1].rotation);
        switch (roads[roads.Count - 1].type)
        {
            case 0:
                {
                    xoff = +12.6f;
                    yoff = 78.5f;
                    curent.position += new Vector3(xoff, yoff, 0);
                        GameObject r = Instantiate(r3, curent.position, r3.transform.rotation) as GameObject;
                        roads.Add(new Road(curent.position.x, curent.position.y, 3, 0));
                        obj.Add(r);
                    break;
                }
            case 1:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                roff = 0;
                                xoff = 12.6f;
                                yoff = 49f;
                                break;
                            }
                        case 1:
                            {
                                roff = 90;
                                xoff = 49f;
                                yoff = -12.6f;
                                break;
                            }
                        case 2:
                            {
                                roff = 180;
                                xoff = -12.7f;
                                yoff = -48.4f;
                                break;
                            }
                        case 3:
                            {
                                roff = -90;
                                xoff = -48.4f;
                                yoff = +12.6f;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    //curent.Rotate(new Vector3(0, 0, roff));
                    GameObject r = Instantiate(r3, curent.position, r3.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, roff));
                        roads.Add(new Road(curent.position.x, curent.position.y, 3, roads[roads.Count - 1].rotation));
                        obj.Add(r);
                    break;
                }
            case 2:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                roff = -90;
                                xoff = -68.3f;
                                yoff = 25.3f;
                                break;
                            }
                        case 1:
                            {
                                roff = 0;
                                xoff = 25.3f;
                                yoff = 68.3f;
                                break;
                            }
                        case 2:
                            {
                                roff = 90;
                                xoff = 68.3f;
                                yoff = -25.3f;
                                break;
                            }
                        case 3:
                            {
                                roff = 180;
                                xoff = -25.3f;
                                yoff = -68.3f;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    // curent.Rotate(new Vector3(0, 0, roff));
                    GameObject r = Instantiate(r3, curent.position, r3.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, roff));
                        roads.Add(new Road(curent.position.x, curent.position.y, 3, (roads[roads.Count - 1].rotation + 3) % 4));
                        obj.Add(r);
                    break;
                }
            case 3:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                roff = 90;
                                xoff = 68.3f;
                                yoff = 0;
                                break;
                            }
                        case 1:
                            {
                                roff = 180;
                                xoff = 0;
                                yoff = -68.3f;
                                break;
                            }
                        case 2:
                            {
                                roff = -90;
                                xoff = -68.3f;
                                yoff = 0f;
                                break;
                            }
                        case 3:
                            {
                                roff = 0;
                                xoff = 0f;
                                yoff = 68.3f;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    // curent.Rotate(new Vector3(0, 0, roff));
                    GameObject r = Instantiate(r3, curent.position, r3.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, roff));
                        roads.Add(new Road(curent.position.x, curent.position.y, 3, (roads[roads.Count - 1].rotation + 1) % 4));
                        obj.Add(r);
                    break;
                }
        }

    }

    void Type4()
    {
        Debug.Log(roads[roads.Count - 1].type + " " + roads[roads.Count - 1].rotation);
        switch (roads[roads.Count - 1].type)
        {
            case 0:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                xoff = 0f;
                                yoff = 88.8f;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    GameObject r = Instantiate(finish, curent.position, finish.transform.rotation) as GameObject;
                    roads.Add(new Road(curent.position.x, curent.position.y, 4, 0));
                    obj.Add(r);

                    break;
                }
            case 1:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                xoff = 0f;
                                yoff = 59f;
                                break;
                            }
                        case 1:
                            {
                                xoff = 59f;
                                yoff = 0f;
                                break;
                            }
                        case 2:
                            {
                                xoff = 0f;
                                yoff = -59f;
                                break;
                            }
                        case 3:
                            {
                                xoff = -59f;
                                yoff = 0;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    GameObject r = Instantiate(finish, curent.position, obj[obj.Count - 1].transform.rotation) as GameObject;
                    roads.Add(new Road(curent.position.x, curent.position.y, 4, roads[roads.Count - 1].rotation));
                    obj.Add(r);
                    break;
                }
            case 2:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                roff = -90;
                                xoff = -78.8f;
                                yoff = 12.8f;
                                break;
                            }
                        case 1:
                            {
                                roff = 0;
                                xoff = 12.8f;
                                yoff = 78.6f;
                                break;
                            }
                        case 2:
                            {
                                roff = 90;
                                xoff = 78.8f;
                                yoff = -12.8f;
                                break;
                            }
                        case 3:
                            {
                                roff = 180;
                                xoff = -12.8f;
                                yoff = -78.8f;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    //curent.Rotate(new Vector3(0, 0, roff));
                    GameObject r = Instantiate(finish, curent.position, finish.transform.rotation) as GameObject;
                    r.transform.Rotate(new Vector3(0, 0, roff));
                    roads.Add(new Road(curent.position.x, curent.position.y, 4, (roads[roads.Count - 1].rotation + 3) % 4));
                    obj.Add(r);
                    break;
                }
            case 3:
                {
                    switch (roads[roads.Count - 1].rotation)
                    {
                        case 0:
                            {
                                roff = +90;
                                xoff = +78.8f;
                                yoff = 12.8f;
                                break;
                            }
                        case 1:
                            {
                                roff = 180;
                                xoff = 12.8f;
                                yoff = -78.6f;
                                break;
                            }
                        case 2:
                            {
                                roff = -90;
                                xoff = -78.8f;
                                yoff = -12.8f;
                                break;
                            }
                        case 3:
                            {
                                roff = 0;
                                xoff = -12.8f;
                                yoff = +78.8f;
                                break;
                            }
                    }
                    curent.position += new Vector3(xoff, yoff, 0);
                    GameObject r = Instantiate(finish, curent.position, finish.transform.rotation) as GameObject;
                    r.transform.Rotate(new Vector3(0, 0, roff));
                    roads.Add(new Road(curent.position.x, curent.position.y, 4, (roads[roads.Count - 1].rotation + 1) % 4));
                    obj.Add(r);
                    break;
                }
        }
        string path = Application.persistentDataPath + "/Text.txt";
        string[] savefile = File.ReadAllLines(path);
        path = Application.persistentDataPath + savefile[0];
        File.WriteAllText(path, "");
        StreamWriter writer = new StreamWriter(path, true);
        for (int i = 0; i < roads.Count; i++)
            writer.WriteLine(roads[i].x.ToString("F2") + " " + roads[i].y.ToString("F2") + " " + roads[i].type.ToString("F2") + " " + roads[i].rotation.ToString("F2"));
        writer.Close();
        Application.LoadLevel("Meniu");
    }

    public void remove()
    {
        if (roads.Count >= 2)
        {
            roads.RemoveAt(roads.Count - 1);
            Destroy(obj[obj.Count - 1]);
            obj.RemoveAt(obj.Count - 1);
            curent.position = new Vector3(roads[roads.Count - 1].x, roads[roads.Count - 1].y, 0.04f);
        }
    }

}
 