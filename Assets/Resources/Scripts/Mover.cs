using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Rigidbody2D>().velocity = Vector3.up * speed;
    }

    /*void Update()
    {
        Debug.Log(GetComponent<Rigidbody2D>().position);
    }*/
}
