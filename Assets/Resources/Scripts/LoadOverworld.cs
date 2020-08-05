using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadOverworld : MonoBehaviour
{
    public Text confirmationText;
    public GameObject LoadingText;

    private GameController controller;

    void Start()
    {
        confirmationText.gameObject.SetActive(false);
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (controller == null)
        {
            Debug.LogError("Unable to find the gameController script");
        }
        LoadingText.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            confirmationText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Y))
            {
                LoadingText.SetActive(true);
                controller.SavePlayerStats();
                SceneManager.LoadScene("Exploration");
            }
        }
    }

   
    void OnTriggerExit2D(Collider2D other)
    {
        confirmationText.gameObject.SetActive(false);
    }
}
