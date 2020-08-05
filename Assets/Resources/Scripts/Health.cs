using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    private AudioClip hitClip;
    public GameObject Death;

    public GameController controller;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (controller == null)
        {
            Debug.LogError("Unable to find the gameController script");
        }
    }

    public void damage(int dam)
    {
        AudioSource audioSrc = GetComponents<AudioSource>()[1];
        currentHP = currentHP < 1 ? 0 : currentHP - dam;
        if (currentHP < 1)
        {
            if (gameObject.tag == "Player")
            {
                controller.EndGame();
            }
            if (gameObject.tag == "Creature")
            {
                controller.AddScore((int)(60/((float)controller.lv/gameObject.GetComponent<EnemyControll>().lv)));
            }
            Destroy(gameObject);
            GameObject tmp = Instantiate(Death, transform.position, transform.rotation);
            Destroy(tmp, 0.7f);
        }
        else
        {
            audioSrc.Play();
        }
    }
}
