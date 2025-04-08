using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int Health;
    public int MaxHealth;
    public AIDestinationSetter dest;

    void Start()
    {
        dest.target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (Health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    // void OnCollisionEnter2D(Collision2D c)
    // {
    //     Debug.Log("Enemy Hit by " + c.gameObject.name);
    //     if(c.gameObject.layer == 6)
    //     {
    //         Health -= c.gameObject.GetComponent<BulletScript>().DMG;
    //     }
    // }
}
