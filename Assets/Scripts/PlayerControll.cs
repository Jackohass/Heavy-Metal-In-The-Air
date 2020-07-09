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
    void Start()
    {
        nextThrow = 0;
    }

    public float speed;
    public Boundry bound;

    void FixedUpdate()
    {
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
    }

    //Throws
    public GameObject throwObj;
    public Transform throwSpawn;
    public float throwRate;
    private float nextThrow;

    void Update()
    {
        if(Input.GetKey("space") && Time.time > nextThrow)
        {
            nextThrow = Time.time + throwRate;
            Instantiate(throwObj, throwSpawn.position, throwSpawn.rotation);
        }
    }
}
