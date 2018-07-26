using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class SaveLoader : MonoBehaviour {

    private void OnApplicationQuit()
    {
        SavePlayer();
    }

    private void Start()
    {
        LoadPlayer();
    }

    public void SavePlayer()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/PlayerData/Snakes");
        foreach (SnakeProfile sp in Player.Instance.Snakes)
        {
            SaveSnake(sp);
        }

        SaveFile("Player.pl", Player.Instance.PlayerData);

        Debug.Log("saved to "+Application.persistentDataPath);
    }

    private void SaveSnake(SnakeProfile sp)
    {
        SaveFile("Snakes/"+sp.NickName.TrimEnd('\r', '\n')+".sss", sp);
    }

    private void SaveFile(string path, object obj)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerData/"+path);
        bf.Serialize(file, obj);
        file.Close();
    }


    public void LoadPlayer()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath + "/PlayerData", "*.pl");

        if (files.Length>0 && File.Exists(files[0]))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(files[0], FileMode.Open);
            PlayerData p = (PlayerData)bf.Deserialize(file);
            file.Close();
            Player.Instance.PlayerData = p;

            var info = new DirectoryInfo(Application.persistentDataPath + "/PlayerData/Snakes");
            var fileInfo = info.GetFiles();
            foreach (FileInfo snakeFile in fileInfo)
            {
                FileStream file2 = File.Open(snakeFile.FullName, FileMode.Open);
                SnakeProfile s = (SnakeProfile)bf.Deserialize(file2);
                Player.Instance.Snakes.Add(s);
                file.Close();
            }
        }

    }
}

