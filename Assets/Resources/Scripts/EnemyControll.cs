using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    public float speed;
    public Boundry bound;
    private bool horDir;
    public int damage;
    public int lv;
    private float deltaTime;

    public void setValues(int lv)
    {
        int hp = Random.Range(40 * lv, 40 * lv + 20);
        GetComponent<Health>().maxHP = hp;
        GetComponent<Health>().currentHP = hp;
        damage = Random.Range(10 * lv, 10 * lv + 11);
        this.lv =  lv;
    }

    void FixedUpdate()
    {
        AudioSource audioSrc = GetComponents<AudioSource>()[0];
        float moveHorizontal = horDir ? 1 : -1;
        /*if(moveHorizontal != 0 || moveVertical != 0)
        {
            Debug.Log("There is player input");
        }*/

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        GetComponent<Rigidbody2D>().velocity = movement * speed;
        if(GetComponent<Rigidbody2D>().position.x < bound.xMin)
        {
            horDir = true;
        }
        else if (GetComponent<Rigidbody2D>().position.x > bound.xMax)
        {
            horDir = false;
        }
        GetComponent<Rigidbody2D>().position = new Vector3(
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, bound.xMin, bound.xMax),
            GetComponent<Rigidbody2D>().position.y,
            0.0f
        );
        if (GetComponent<Rigidbody2D>().velocity.y != 0 || GetComponent<Rigidbody2D>().velocity.x != 0)
        {
            if (!audioSrc.isPlaying)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Creature")
        {
            horDir = !horDir;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nextThrow = 0;
        horDir = (Random.value >= 0.5);
        deltaTime = Random.value;
    }

    public GameObject throwObj;
    public Transform throwSpawn;
    public float throwRate;
    private float nextThrow;
    void Update()
    {
        if (Time.time > nextThrow + deltaTime)
        {
            nextThrow = Time.time + throwRate;
            GameObject go = Instantiate(throwObj, throwSpawn.position, throwSpawn.rotation);
            go.GetComponent<ContactDestroy>().defineVals(damage, "Player");
        }
    }
}
