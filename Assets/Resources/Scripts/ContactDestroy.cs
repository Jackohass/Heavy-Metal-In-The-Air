using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDestroy : MonoBehaviour
{
    private int damage;
    private string enemy;

    void Start()
    {
        
    }

    public void defineVals(int damage, string toHit)
    {
        this.damage = damage;
        enemy = toHit;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == enemy)
        {
            other.gameObject.GetComponent<Health>().damage(damage);
            Destroy(gameObject);
        }
    }
}
