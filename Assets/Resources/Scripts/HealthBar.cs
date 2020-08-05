using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health player;
    public Text HPText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        if (player == null)
        {
            Debug.LogError("Unable to find the player script");
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3((float)player.currentHP / player.maxHP, 1.0f, 1.0f);
        HPText.text = player.currentHP + "/" + player.maxHP;
    }
}
