using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundry
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerControll : MonoBehaviour
{
    public int lv;
    public void leveUp(int lv)
    {
        int newHP = lv * 50;
        GetComponent<Health>().maxHP = newHP;
        GetComponent<Health>().currentHP = newHP;
        damage = lv * 10;
        this.lv = lv;
    }

    public GameObject[] weaponUI;
    public GameObject[] weaponUITextures;

    void Start()
    {
        nextThrow = 0;
        playerWeapons[2].hasWeapon = true;
        selectedWeapon = playerWeapons[2];
        for(int i = 0; i < 4; i++)
        {
            if(!playerWeapons[i].hasWeapon)
            {
                weaponUI[i].SetActive(false);
                weaponUITextures[i].SetActive(false);
            }
        }
        weaponUI[2].SetActive(true);
        weaponUITextures[2].SetActive(true);

    }

    public float speed;
    public Boundry bound;
    public int damage;

    void FixedUpdate()
    {
        AudioSource audioSrc = GetComponents<AudioSource>()[0];
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        /*if(moveHorizontal != 0 || moveVertical != 0)
        {
            Debug.Log("There is player input");
        }*/

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        GetComponent<Rigidbody2D>().velocity = movement * speed;

        GetComponent<Rigidbody2D>().position = new Vector3(
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, bound.xMin, bound.xMax),
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, bound.yMin, bound.yMax),
            0.0f
        );
        if(GetComponent<Rigidbody2D>().velocity.y != 0 || GetComponent<Rigidbody2D>().velocity.x != 0)
        {
            if(!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
        }
        else
        {
            if (audioSrc.isPlaying)
            {
                audioSrc.Stop();
            }
        }
    }

    [System.Serializable]
    public class PlayerThrowWeapon
    {
        public GameObject objThrow;
        public int damageMod;
        public float throwRate;
        public bool hasWeapon;

        public PlayerThrowWeapon()
        {
            hasWeapon = false;
        }

        public float throwWeapon(float nextThrow, int damage, Transform throwSpawn, int lv)
        {
            if(Time.time > nextThrow)
            {
                GameObject go = Instantiate(objThrow, throwSpawn.position, throwSpawn.rotation);
                go.GetComponent<ContactDestroy>().defineVals(damage + damageMod*lv, "Creature");
                return Time.time + throwRate;
            }
            return nextThrow;
        }
    }

    public void addWeaponAtIndex(int index)
    {
        playerWeapons[index].hasWeapon = true;
        weaponUI[index].SetActive(true);
        weaponUITextures[index].SetActive(true);
    }

    public Transform throwSpawn;
    private float nextThrow;
    //0: Sword, 1: Dagger, 2: Axe, 3: mace
    public PlayerThrowWeapon[] playerWeapons; 
    private PlayerThrowWeapon selectedWeapon;

    void Update()
    {
        if(Input.GetKey("space"))
        {
            nextThrow = selectedWeapon.throwWeapon(nextThrow, damage, throwSpawn, lv);
        }
        if (Input.GetKey(KeyCode.Alpha1) && playerWeapons[2].hasWeapon) // Axe
        {
            selectedWeapon = playerWeapons[2];
        }
        else if (Input.GetKey(KeyCode.Alpha2) && playerWeapons[0].hasWeapon) // Sword
        {
            selectedWeapon = playerWeapons[0];
        }
        else if (Input.GetKey(KeyCode.Alpha3) && playerWeapons[1].hasWeapon) // Dagger
        {
            selectedWeapon = playerWeapons[1];
        }
        else if (Input.GetKey(KeyCode.Alpha4) && playerWeapons[3].hasWeapon) // Mace
        {
            selectedWeapon = playerWeapons[3];
        }
    }
}
