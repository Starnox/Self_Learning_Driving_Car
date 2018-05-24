using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class track_constructor : MonoBehaviour {

    // Use this for initialization
    public GameObject start, r1, r2, r3, finish;

	void Start () {
        string path = Application.persistentDataPath + "/Text.txt";
        string[] savefile = File.ReadAllLines(path);
        path = Application.persistentDataPath + savefile[2];
        string[] weights = File.ReadAllLines(path);
        float[] v = new float[4];
        for (int i = 0; i < weights.Length; i++)
        {

            int j = 0, nr, sign;
            for (int x = 0; x < 4; x++)
            {
                nr = 0;
                sign = 1;

                while (j < weights[i].Length && weights[i][j] != ' ')
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
                }
                j++;
                v[x] = (float)(nr * sign) / 100;
            }
            switch ((int)v[2])
            {
                case 0:
                    {
                        GameObject r = Instantiate(start, new Vector3(v[0], v[1], 0.3f), start.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, v[3] * 90));
                        break;
                    }
                case 1:
                    {
                        GameObject r = Instantiate(r1, new Vector3(v[0], v[1], 0.3f), r1.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, v[3] * 90));
                        break;
                    }
                case 2:
                    {
                        GameObject r = Instantiate(r2, new Vector3(v[0], v[1], 0.3f), r2.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, v[3] * 90));
                        break;
                    }
                case 3:
                    {
                        GameObject r = Instantiate(r3, new Vector3(v[0], v[1], 0.3f), r3.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, v[3] * 90));
                        break;
                    }
                case 4:
                    {
                        GameObject r = Instantiate(finish, new Vector3(v[0], v[1], 0.3f), finish.transform.rotation) as GameObject;
                        r.transform.Rotate(new Vector3(0, 0, v[3] * 90));
                        break;
                    }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
