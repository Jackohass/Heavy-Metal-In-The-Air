using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverworldGameController : MonoBehaviour
{
    public GameObject LoadingText;
    public Text endConfirmationText;

    private AudioClip backgroundMusic;

    void Start()
    {
        LoadingText.SetActive(false);
        playerPressedEsc = false;
        endConfirmationText.gameObject.SetActive(false);

        int randInt = Random.Range(0, 3);
        switch (randInt)
        {
            case 0:
                backgroundMusic = Resources.Load<AudioClip>("Audio/Background music/alexander-nakarada-adventure");
                if (backgroundMusic == null)
                {
                    Debug.LogError("Can't find psycadelic music");
                }
                break;
            case 1:
                backgroundMusic = Resources.Load<AudioClip>("Audio/Background music/alexander-nakarada-now-we-ride");
                if (backgroundMusic == null)
                {
                    Debug.LogError("Can't find jazz music");
                }
                break;
            case 2:
                backgroundMusic = Resources.Load<AudioClip>("Audio/Background music/alexander-nakarada-nowhere-land");
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

    private bool playerPressedEsc;

    void Update()
    {
        if (playerPressedEsc)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
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

    public void loadingAdventure()
    {
        LoadingText.SetActive(true);
    }
}
