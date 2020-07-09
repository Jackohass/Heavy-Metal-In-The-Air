using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundryDestroy : MonoBehaviour
{
    //TEST Thing:
    public GameController controller;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (controller == null)
        {
            Debug.LogError("Unable to find the gameController script");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Throw")
        {
            controller.AddScore(10);
        }
        Destroy(other.gameObject);
        //Debug.Log("I am triggered")

        //TEST Thing:
        if(Input.GetKey("space"))
        {
            controller.EndGame();
        }
    }
}
