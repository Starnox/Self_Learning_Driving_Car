using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class menu : MonoBehaviour {
    public int x,y,genome,s;
    public Dropdown d;
    void Start()
    {
        
    }
    private void Update()
    {
        genome = d.value + 1;
    }
    public void quit()
    {
        Application.Quit();
    }
    public void save(int x)
    {
        s = x;
    }
    public void new_game()
    {
        x = 1;
    }
    public void load()
    {
        x = 0;
    }
    public void play()
    {
        if (s == 1)
            beat_1();
        else if (s == 2)
            beat_2();
        else beat_3();
        Application.LoadLevel("beat_AI");
    }
    public void beat_1()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        File.WriteAllText(path, "/save_1.txt");
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.Write(genome);
        writer.Close();
    }
    public void beat_2()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        File.WriteAllText(path, "/save_2.txt");
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.Write(genome);
        writer.Close();
    }
    public void beat_3()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        File.WriteAllText(path, "/save_3.txt");
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.Write(genome);
        writer.Close();
    }
    public void train_4()
    {
        Application.LoadLevel("test");
    }
    public void train_1()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.WriteLine("/track_save_1.txt");
        writer.Close();
        Application.LoadLevel("train_track");
    }
    public void train_2()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.WriteLine("/track_save_2.txt");
        writer.Close();
        Application.LoadLevel("train_track");
    }
    public void train_3()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.WriteLine("/track_save_3.txt");
        writer.Close();
        Application.LoadLevel("train_track");
    }
    public void save_1()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        File.WriteAllText(path, "/save_1.txt");
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.Write(x);
        writer.Close();
        
    }
    public void save_2()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        File.WriteAllText(path, "/save_2.txt");
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.Write(x);
        writer.Close();
    }
    public void save_3()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        File.WriteAllText(path, "/save_3.txt");
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.Write(x);
        writer.Close();
    }
    public void track_new_game()
    {
        y = 1;
    }
    public void track_load()
    {
        y = 0;
    }
    public void track_save_1()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        File.WriteAllText(path, "/track_save_1.txt");
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.Write(y);
        writer.Close();
        Application.LoadLevel("own_track");
    }
    public void track_save_2()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        File.WriteAllText(path, "/track_save_2.txt");
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.Write(y);
        writer.Close();
        Application.LoadLevel("own_track");
    }
    public void track_save_3()
    {
        string path = Application.persistentDataPath + "/Text.txt";
        File.WriteAllText(path, "/track_save_3.txt");
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine();
        writer.Write(y);
        writer.Close();
        Application.LoadLevel("own_track");
    }
}
