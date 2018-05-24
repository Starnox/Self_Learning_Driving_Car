using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class setup_AI : MonoBehaviour {
    private AIcontroller script;
    public GameObject car;
    int gen;
	void Start () {
        script = car.GetComponent<AIcontroller>();
        string path = Application.persistentDataPath + "/Text.txt";
        string[] savefile = File.ReadAllLines(path);
        path = Application.persistentDataPath + savefile[0];
        string[] weights = File.ReadAllLines(path);
        Debug.Log(path);
        Debug.Log(savefile[1]);
        int q = 0, nou = 0;
        while (q < savefile[1].Length)
        {
            nou = nou * 10 + savefile[1][q] - '0';
            q++;
        }
        gen = nou;
        Debug.Log(gen);
        int sign, nr, j = 0, i = gen*2-1;
                for (int l = 1; l <= 3; l++)
                    for (int ii = 1; ii <= 4; ii++)
                        for (int jj = 1; jj <= 4; jj++)
                        {
                            nr = 0;
                            sign = 1;
                            while ( j < weights[i].Length && weights[i][j] != ' ' )
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
                            script.a[l, ii, jj] = (float)(nr * sign) / 10000;
                            Debug.Log(script.a[l, ii, jj]);
                        }
    }
	
	void Update () {
		
	}
}
