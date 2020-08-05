using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworldControll : MonoBehaviour
{
    public float speed;
    public Boundry bound;

    void FixedUpdate()
    {
        AudioSource audioSrc = GetComponent<AudioSource>();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        GetComponent<Rigidbody2D>().velocity = movement * speed;

        GetComponent<Rigidbody2D>().position = new Vector3(
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, bound.xMin, bound.xMax),
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, bound.yMin, bound.yMax),
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
}
