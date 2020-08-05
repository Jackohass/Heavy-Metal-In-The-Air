﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundryDestroy : MonoBehaviour
{

    void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
        //Debug.Log("I am triggered")
    }
}
