using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public Transform spawn1;
    public Transform spawn2;
    public GameObject enemyPrefab;
    public GameObject Wall;
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.CompareTag("Player"))
        {
            GameObject.Instantiate(enemyPrefab, spawn1);
            GameObject.Instantiate(enemyPrefab, spawn2);
            Wall.SetActive(true);
        }
    }
}
