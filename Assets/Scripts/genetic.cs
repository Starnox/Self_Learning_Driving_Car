using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class genetic : MonoBehaviour {

    private int generation;
    public Text mut;
    public Text t1, t2;
    private float[,,,] a, rez;
    private float max1, max2;
    private int cur, i1, i2;
    private AIcontroller script;
    public GameObject car;
    public bool col;
    public float fitness;
    private float[] fit;
    private Rigidbody2D rb;
    private float[] probabilities;
    private int[] alive;
    private int[] ales;
    public float mutationRate;
    public Slider slider;
    public string sv;

    void Start() {
        rb = car.GetComponent<Rigidbody2D>();
        col = false;
        cur = 1;
        max1 = 0f;
        max2 = 0f;
        generation = 1;
        alive = new int[13];
        fit = new float[13];
        a = new float[13, 4, 5, 5];
        rez = new float[13, 4, 5, 5];
        probabilities = new float[13];
        ales = new int[13];

        script = car.GetComponent<AIcontroller>();

        string path = Application.persistentDataPath + "/Text.txt";
        string[] savefile = File.ReadAllLines(path);
        if (savefile[1][0] - '0' == 1)
            random();
        else 
            load();

        for (int l = 1; l <= 3; l++)
            for (int i = 1; i <= 4; i++)
                for (int j = 1; j <= 4; j++)
                    script.a[l, i, j] = a[cur, l, i, j];
    }


    void Update() {
        mutationRate = slider.value;
        mut.text = "Mutation Rate: " + mutationRate.ToString("0.0000");
        t1.text = "Genome: " + cur.ToString();
        t2.text = "Generation: " + generation.ToString();
        if (col == true)
        {

            if (cur < 12)
            {
                fit[cur] = fitness;
                cur++;
                for (int l = 1; l <= 3; l++)
                    for (int i = 1; i <= 4; i++)
                        for (int j = 1; j <= 4; j++)
                        {
                            script.a[l, i, j] = a[cur, l, i, j];
                            
                        }
                car.transform.position = new Vector3(0, 0, -2);
                rb.velocity = new Vector3(0, 0, 0);
                rb.transform.rotation = new Quaternion(0, 0, 180, 0);
                col = false;

            }
            else if (cur == 12)
            {
                save();
                fit[cur] = fitness;
                Selection();
                kill();
                crosbreed();
                mutation();
                reset();
                generation++;
                cur = 1;
                Debug.Log(cur - 1 + " " + fit[cur - 1]);
                for (int l = 1; l <= 3; l++)
                    for (int i = 1; i <= 4; i++)
                        for (int j = 1; j <= 4; j++)
                        {
                            script.a[l, i, j] = a[cur, l, i, j];
                        }
                car.transform.position = new Vector3(0, 0, -2);
                rb.velocity = new Vector3(0, 0, 0);
                rb.transform.rotation = new Quaternion(0, 0, 180, 0);
                col = false;
            }
            Debug.Log(cur - 1 + " " + fit[cur - 1]);
        }
    }

    void save()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        string[] savefile = File.ReadAllLines(path);
         path = Application.persistentDataPath + savefile[0];
        File.WriteAllText(path, "");
        StreamWriter writer = new StreamWriter(path,true);

        for (int m = 1; m <= 12; m++)
        {
            writer.WriteLine(m);
            for (int l = 1; l <= 3; l++)
                for (int i = 1; i <= 4; i++)
                    for (int j = 1; j <= 4; j++)
                        writer.Write(a[m, l, i, j].ToString("F4") + " ");
            writer.WriteLine();
        }
        writer.Write(generation);
        writer.Close();

     }

    void load()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        string[] savefile = File.ReadAllLines(path);
        path = Application.persistentDataPath + savefile[0];
        string[] weights = File.ReadAllLines(path);
        Debug.Log(path);
        for(int i=0; i <weights.Length-1;i++)
        {    
            if(i % 2 == 0)
            {
                int j = 0, nr = 0, sign=1,ord;
                while (j < weights[i].Length)
                {
                    nr = nr * 10 + weights[i][j] - '0';
                    j++;
                }
                ord = nr;
                i++;
                j = 0;
                  for (int l = 1; l <= 3; l++)
                      for (int ii = 1; ii <= 4; ii++)
                          for (int jj = 1; jj <= 4; jj++)
                          {
                            nr = 0;
                            sign = 1;
                            while (weights[i][j] != ' ' && j<weights[i].Length)
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
                            a[ord, l, ii, jj] = (float)(nr * sign) / 10000;
                        }
            }
        }
        int q = 0, nou = 0;
        while (q < weights[weights.Length-1].Length)
        {
            nou = nou * 10 + weights[weights.Length - 1][q] - '0';
            q++;
        }
        generation = nou+1;
    }

    void random()
    {
        for (int m = 1; m <= 12; m++)
        {
            for (int l = 1; l <= 3; l++)
                for (int i = 1; i <= 4; i++)
                    for (int j = 1; j <= 4; j++)
                    {
                        a[m, l, i, j] = Random.Range(-10f, 10f);
                    }
        }
    }

    void reset()
    {
        for (int i = 1; i <= 12; i++)
        {
            ales[i] = 0;
            alive[i] = 0;
        }
       /* for (int m = 1; m <= 12; m++)
        {
            string path = "Assets/Text.txt";

            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(m+ " " + fit[m]);

            float[] v = new float[50];
            for (int l = 1; l <= 3; l++)
                for (int i = 1; i <= 4; i++)
                    for (int j = 1; j <= 4; j++)
                        writer.Write(a[m,l,i,j] + " ");
            writer.WriteLine();
            writer.Close();
        }*/
    }


    /*void crosbreed()
    {
        int nr = 1;
            for (int l = 1; l <= 3; l++)
                for (int i = 1; i <= 4; i++)
                    for (int j = 1; j <= 4; j++)
                    {
                        float aux = a[1, l, i, j];
                        a[1, l, i, j] = a[i1, l, i, j];
                        a[i1, l, i, j] = aux;
                    }
            for (int l = 1; l <= 3; l++)
                for (int i = 1; i <= 4; i++)
                    for (int j = 1; j <= 4; j++)
                    {
                        float aux = a[2, l, i, j];
                        a[2, l, i, j] = a[i2, l, i, j];
                        a[i2, l, i, j] = aux;
                    }
        while (nr < 6)
        {

            reproduce(1, 2, 2 * nr +1);
            nr++; 
            Debug.Log("Perechi: " + i1 + " " + i2);
        }
    }*/


     void crosbreed()
     {
         int nr = 0;
         while (nr < 3)
         {
             float ran = Random.Range(0f, 1f);
             int index1 = cb(ran);
             if (alive[index1] == 1)
             {
                 int index2;
                 do
                 {
                     ran = Random.Range(0f, 1f);
                     index2 = cb(ran);
                 }
                 while (alive[index2] == 0 || index1 == index2);
                 nr++;
                 Debug.Log("Perechi: " + index1 + " " + index2);
                 reproduce(index1, index2);
             }
         }
     }

    void reproduce(int ind1, int ind2)
    {
        float[,,] child1 = new float[4, 5, 5], child2 = new float[4, 5, 5];
        bool ok = false;
         for (int l = 1; l <= 3; l++)
             for (int i = 1; i <= 4; i++)
                 for (int j = 1; j <= 4; j++)
                 {
                     float ran = Random.Range(0f, 1f);
                     if (ran < 0.5)
                     {
                         child1[l, i, j] = a[ind1, l, i, j];
                         child2[l, i, j] = a[ind2, l, i, j];
                     }
                     else
                     {
                         child2[l, i, j] = a[ind1, l, i, j];
                         child1[l, i, j] = a[ind2, l, i, j];
                     }
                 }

       /* for (int l = 1; l <= 3; l++)
            for (int i = 1; i <= 4; i++)
                for (int j = 1; j <= 4; j++)
                {
                    a[poz, l, i, j] = child1[l, i, j];
                }

        for (int l = 1; l <= 3; l++)
            for (int i = 1; i <= 4; i++)
                for (int j = 1; j <= 4; j++)
                {
                    a[poz+1, l, i, j] = child2[l, i, j];
                }*/
       /*int ran = Random.Range(1, 48);
        int ct = 0;
        for (int l = 1; l <= 3; l++)
            for (int i = 1; i <= 4; i++)
                for (int j = 1; j <= 4; j++)
                {
                    ct++;
                    if (ct <= ran)
                    {
                        child1[l, i, j] = a[ind1, l, i, j];
                        child2[l, i, j] = a[ind2, l, i, j];
                    }
                    else
                    {
                        child1[l, i, j] = a[ind2, l, i, j];
                        child2[l, i, j] = a[ind1, l, i, j];
                    }
                }*/
       for (int q = 1; q <= 12; q++)
            if (alive[q] == 0)
            {
                alive[q] = 1;
                Debug.Log(q);
                if (ok == false)
                {
                    for (int l = 1; l <= 3; l++)
                        for (int i = 1; i <= 4; i++)
                            for (int j = 1; j <= 4; j++)
                            {
                                a[q, l, i, j] = child1[l, i, j];
                            }
                    ok = true;
                }
                else
                {
                    for (int l = 1; l <= 3; l++)
                        for (int i = 1; i <= 4; i++)
                            for (int j = 1; j <= 4; j++)
                            {
                                a[q, l, i, j] = child2[l, i, j];
                            }
                    return;
                }
                
            }
        
    }

    void mutation()
    {
        for (int q = 1; q <= 12; q++)
            for (int l = 1; l <= 3; l++)
                for (int i = 1; i <= 4; i++)
                    for (int j = 1; j <= 4; j++)
                    {
                        float ran = Random.Range(0f, 1f);
                        if (ran < mutationRate)
                        {

                            int ran2 = Random.Range(0, 12);
                            int numar = (int)(a[q, l, i, j] * 10000);
                            numar ^= (1 << ran2);
                            Debug.Log("Mutatie: " + q + " " + a[q, l, i, j] + " " + (float)numar / 10000);
                            a[q, l, i, j] = numar / 10000;

                        }
                    }
    }

    int cb(float x)
    {
        for (int i = 1; i <= 12; i++)
        {
            if (x < probabilities[i])
                return i;
        }
        return 1;
    }

    void kill()
    {
        int nr = 0;
        while (nr < 6)
        {
            float ran = Random.Range(0f, 1f);
            int index = cb(ran);
            
            if (alive[index] == 0)
            {
                Debug.Log("Traieste: " + index);
                nr++;
                alive[index] = 1;
            }
        }
    }

    float Sum()
    {
        float s = 0;
        for (int i = 1; i <= 12; i++)
        {
            s += fit[i];
        }
        return s;
    }

     void Selection()
     {
         float totalFitness = Sum();

         float s = 0;

         for (int i = 1; i <= 12; i++)
         {
             s += fit[i] / totalFitness;
             probabilities[i] = s;
         }

         probabilities[12] = 1;
      }

   /* void Selection()
    {
        for(int i=0; i<=12; i++)
        {
            if(fit[i]>max1)
            {
                max2 = max1;
                max1 = fit[i];
                i2 = i1;
                i1 = i;
            }
            else if(fit[i]>max2)
            {
                max2 = fit[i];
                i2 = i;
            }
        }
    }*/
}
