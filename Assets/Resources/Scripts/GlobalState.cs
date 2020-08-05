using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GlobalState : MonoBehaviour
{
    public static GlobalState instance;
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            setStartValues();
        }
        else if (instance != this)
        {
            instance.placePlaces();
            Destroy(gameObject);
        }
    }

    public int lv;
    public bool[] weaponIndexFound;
    public int xp;

    public Save save;

    public LocationInfo[][] locations;
    public LocationInfo currentLocation;
    private const int width = 25;
    private const int height = 13;

    public Vector3 placeAt;
    public GameObject place;

    private void setStartValues()
    {
        weaponIndexFound = new bool[4];
        weaponIndexFound[2] = true;
        lv = 1;
        locations = new LocationInfo[width][];

        for (int i = 0; i < width; i++)
        {
            locations[i] = new LocationInfo[height];
            for (int j = 0; j < height; j++)
            {
                locations[i][j] = Instantiate(place, new Vector3(
                    placeAt.x + i*0.71f,
                    placeAt.y - j*0.75f,
                    placeAt.z), Quaternion.identity).GetComponent<Location>().GetLocationInfo();
            }
        }
    }
    
    public void placePlaces()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(place, new Vector3(
                    placeAt.x + i * 0.71f,
                    placeAt.y - j * 0.75f,
                    placeAt.z), Quaternion.identity).GetComponent<Location>().setLocation(locations[i][j]);
            }
        }
    }

    public void EndCurrentGame()
    {
        SaveScore();
        Destroy(instance.gameObject);
        instance = null;
    }

    private void SaveScore()
    {
        Save save;
        if (File.Exists(Application.persistentDataPath + "/scores.veryspecificbinaryfiletype"))
        {
            FileStream loadfile = File.Open(Application.persistentDataPath + "/scores.veryspecificbinaryfiletype", FileMode.Open);
            save = (Save)(new BinaryFormatter()).Deserialize(loadfile);
            loadfile.Close();

        }
        else
        {
            save = new Save();
        }
        Save.ScoreEntry scory = new Save.ScoreEntry();
        scory.name = "Try " + save.scoreList.Count + ": ";
        scory.score = xp;
        save.scoreList.Add(scory);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream savefile = File.Create(Application.persistentDataPath + "/scores.veryspecificbinaryfiletype");
        bf.Serialize(savefile, save);
        savefile.Close();
    }
}
