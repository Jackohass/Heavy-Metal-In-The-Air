using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public PlayerControll player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();
        if (player == null)
        {
            Debug.LogError("Unable to find the player script");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            player.currentHP -= 1;
            transform.localScale = new Vector3((float)player.currentHP / player.maxHP, 1.0f, 1.0f);
        }
    }
}
