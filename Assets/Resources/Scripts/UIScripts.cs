using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{
    public GameObject volumeSlider;

    public void Awake()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            float oldVolume = PlayerPrefs.GetFloat("Volume");
            AudioListener.volume = oldVolume;
            volumeSlider.GetComponent<Slider>().value = oldVolume;
        }
        else 
        {
            AudioListener.volume = 1.0f;
            PlayerPrefs.SetFloat("Volume", 1.0f);
            PlayerPrefs.Save();
        }
    }
    public void OnButtonPressCredit()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnButtonPressPlayGame()
    {
        SceneManager.LoadScene("Exploration");
    }

    public void OnButtonPressGoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void AdjustVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
        PlayerPrefs.SetFloat("Volume", newVolume);
    }

    public void OnButtonPressQuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
