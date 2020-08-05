using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnemySeed
{
    public Vector3 spawnAt;
    public int type;
    public int lv;
}

[System.Serializable]
public class ChestSeed
{
    public Vector3 spawnAt;
}

[System.Serializable]
public class WallSeed
{
    public Vector3 spawnAt;
    public int type;
}

[System.Serializable]
public class LocationInfo
{
    public int x;
    public int y;
    public ChestSeed chest;
    public bool chestTaken;
    public EnemySeed[] enemies;
    public WallSeed[] walls;
    public Color colour;
    public bool beenVisited;
}

public class Location : MonoBehaviour
{
    public int lv;
    private LocationInfo LocInfo;
    private int type;
    public OverworldGameController controller;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<OverworldGameController>();
        if (controller == null)
        {
            Debug.LogError("Unable to find the gameController script");
        }
    }

    public void setLocation(LocationInfo LI)
    {
        LocInfo = LI;
        if(LocInfo.beenVisited)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.51f, 0f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = LocInfo.colour;
        }
    }

    public LocationInfo GetLocationInfo()
    {
        LocInfo = new LocationInfo();
        int randInt = Random.Range(0, 11);
        if (randInt > -1 && randInt < 5)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.2131273f, 0.9150943f, 0f);
            type = 0;
        }
        else if (randInt > 4 && randInt < 10)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7075472f, 0.7075472f, 0.7075472f);
            type = 1;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.634f, 0.253f, 0f);
            type = 2;
        }
        LocInfo.colour = gameObject.GetComponent<SpriteRenderer>().color;
        LocInfo.beenVisited = false;
        return LocInfo;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKey("space"))
        {
            controller.loadingAdventure();
            if(!LocInfo.beenVisited)
            {
                lv = GlobalState.instance.lv;
                setObjects();
            }
            GlobalState.instance.currentLocation = LocInfo;
            LocInfo.beenVisited = true;
            SceneManager.LoadScene("Adventure");
        }
    }
    
    public Vector3 spawnValues;

    private void setObjects()
    {
        Vector3 spawnAt;
        int numEnemies = Random.Range(1, Mathf.Min(lv + 1, 10));
        LocInfo.enemies = new EnemySeed[numEnemies];
        float deltaX = (spawnValues.x * 2 - numEnemies) / numEnemies;
        for (int i = 0; i < numEnemies; i++)
        {
            spawnAt = new Vector3(Random.Range(i + i * deltaX - spawnValues.x, i + (i + 1) * deltaX - spawnValues.x), spawnValues.y, spawnValues.z);
            int enemyI = Random.Range(0, 3); //There are 3 enemy types
            LocInfo.enemies[i] = new EnemySeed();
            LocInfo.enemies[i].spawnAt = spawnAt;
            LocInfo.enemies[i].lv = (lv + numEnemies*lv) / numEnemies;
            LocInfo.enemies[i].type = enemyI;
        }
        LocInfo.walls = new WallSeed[2];
        spawnAt = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y - 1.6f, spawnValues.z);
        LocInfo.walls[0] = new WallSeed();
        LocInfo.walls[0].spawnAt = spawnAt;
        LocInfo.walls[0].type = type;
        spawnAt = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), -spawnAt.y, spawnValues.z);
        LocInfo.walls[1] = new WallSeed();
        LocInfo.walls[1].spawnAt = spawnAt;
        LocInfo.walls[1].type = type;

        spawnAt = new Vector3(Random.Range(-spawnValues.x + 7.8f, spawnValues.x + 0.2f), spawnValues.y + 0.6f, spawnValues.z);
        LocInfo.chest = new ChestSeed();
        LocInfo.chest.spawnAt = spawnAt;
        LocInfo.chestTaken = false;
    }
}
