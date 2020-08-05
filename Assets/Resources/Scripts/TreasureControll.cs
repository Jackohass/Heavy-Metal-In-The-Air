using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureControll : MonoBehaviour
{
    private List<int> listMissingWeapons;
    private PlayerControll plCtrl;
    public GameObject WeaponText;
    public GameObject XPText;
    public GameController controller;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (controller == null)
        {
            Debug.LogError("Unable to find the gameController script");
        }

        listMissingWeapons = new List<int>();
        plCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();
        if (plCtrl == null)
        {
            Debug.LogError("Unable to find the PlayerControll script");
        }
        for (int i = 0; i < plCtrl.playerWeapons.Length; i++)
        {
            if(!plCtrl.playerWeapons[i].hasWeapon)
            {
                listMissingWeapons.Add(i);
            }
        }
        GameObject[] treasureTexts = GameObject.FindGameObjectsWithTag("TreasureText");
        if (treasureTexts == null)
        {
            Debug.LogError("Unable to find the Treasure Texts script");
        }
        if(treasureTexts[0].name == "TextTreasureXP")
        {
            WeaponText = treasureTexts[1];
            XPText = treasureTexts[0];
        }
        else
        {
            WeaponText = treasureTexts[0];
            XPText = treasureTexts[1];
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            if (plCtrl.playerWeapons.Length > 0 && Random.Range(0, 1.0f) > 0.5f)
            {
                WeaponText.GetComponent<Fade>().fadeinfadout();
                int index = listMissingWeapons[(Random.Range(0, listMissingWeapons.Count))];
                plCtrl.addWeaponAtIndex(index);
            }
            else
            {
                XPText.GetComponent<Fade>().fadeinfadout();
                controller.AddScore(Random.Range(20, 100));
            }
            GlobalState.instance.currentLocation.chestTaken = true;
            Destroy(gameObject, 0.24f);
            
        }
    }
}
