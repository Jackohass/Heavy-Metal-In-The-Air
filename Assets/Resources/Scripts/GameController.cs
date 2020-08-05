using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    public Text xpText;
    public int xp;

    public Text lvText;
    public int lv;

    public GameObject player;
    public LocationInfo thisLocation;

    public Text endText;
    private bool gameEnded;

    public Text endConfirmationText;

    private AudioClip backgroundMusic;

    public GameObject chestType;
    public GameObject[] enemyTypes;
    public GameObject[] wallTypes;

    // Start is called before the first frame update
    void Start()
    {
        xp = GlobalState.instance.xp;
        lv = GlobalState.instance.lv;
        for (int i = 0; i < player.GetComponent<PlayerControll>().playerWeapons.Length; i++)
        {
            if(GlobalState.instance.weaponIndexFound[i])
            {
                player.GetComponent<PlayerControll>().addWeaponAtIndex(i);
            }
        }

        xpText.text = "Xp: " + xp + "/" + 100 * lv;
        lvText.text = "Level: " + lv;
        player.GetComponent<PlayerControll>().leveUp(lv);

        playerPressedEsc = false;
        endConfirmationText.gameObject.SetActive(false);

        gameEnded = false;
        endText.gameObject.SetActive(false);

        thisLocation = GlobalState.instance.currentLocation;

        SpawnEverything();

        int randInt = Random.Range(0, 3); 
        switch(randInt)
        {
            case 0:
                backgroundMusic = Resources.Load<AudioClip>("Audio/Background music/Where_the_Moon_Came_From_-_01_-_Psychedelic_Saturday");
                if(backgroundMusic == null)
                {
                    Debug.LogError("Can't find psycadelic music");
                }
                break;
            case 1:
                backgroundMusic = Resources.Load<AudioClip>("Audio/Background music/New York, 1924");
                if (backgroundMusic == null)
                {
                    Debug.LogError("Can't find jazz music");
                }
                break;
            case 2:
                backgroundMusic = Resources.Load<AudioClip>("Audio/Background music/alexander-nakarada-now-we-ride-metal-version");
                if (backgroundMusic == null)
                {
                    Debug.LogError("Can't find metal music");
                }
                break;
            default:
                Debug.LogError("Something is wrong with the Random.Range()");
                break;
        }
        GetComponent<AudioSource>().clip = backgroundMusic;
        GetComponent<AudioSource>().Play();
    }

    public void AddScore(int points)
    {
        xp += points;
        if (xp > 100 * lv)
        {
            lv++;
            lvText.text = "Level: " + lv;
            player.GetComponent<PlayerControll>().leveUp(lv);
        }
        xpText.text = "Xp: " + xp + "/" + 100 * lv;
    }

    private bool playerPressedEsc;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Adventure");
        }
        if (gameEnded && Input.GetKeyDown(KeyCode.Escape))
        {
            GlobalState.instance.EndCurrentGame();
            SceneManager.LoadScene("Menu");
            return;
        }
        if(playerPressedEsc)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SavePlayerStats();
                GlobalState.instance.EndCurrentGame();
                SceneManager.LoadScene("Menu");
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                playerPressedEsc = false;
                endConfirmationText.gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerPressedEsc = true;
            endConfirmationText.gameObject.SetActive(true);
        }
    }

    public void EndGame()
    {
        gameEnded = true;
        endText.gameObject.SetActive(true);
        SavePlayerStats();
    }

    void SpawnEverything()
    {
        for (int i = 0; i < thisLocation.enemies.Length; i++)
        {
            Instantiate(enemyTypes[thisLocation.enemies[i].type], thisLocation.enemies[i].spawnAt, Quaternion.identity)
                .GetComponent<EnemyControll>().setValues(thisLocation.enemies[i].lv);
        }
        for (int i = 0; i < 2; i++)
        {
            Instantiate(wallTypes[thisLocation.walls[i].type], thisLocation.walls[i].spawnAt, Quaternion.Euler(0,0,180*(i-1)));
        }
        if(!thisLocation.chestTaken)
        {
            Instantiate(chestType, thisLocation.chest.spawnAt, Quaternion.identity);
        }
    }

    public void SavePlayerStats()
    {
        GlobalState.instance.lv = lv;
        GlobalState.instance.xp = xp;
        for (int i = 0; i < player.GetComponent<PlayerControll>().playerWeapons.Length; i++)
        {
            GlobalState.instance.weaponIndexFound[i] = player.GetComponent<PlayerControll>().playerWeapons[i].hasWeapon;
        }
    }
}
