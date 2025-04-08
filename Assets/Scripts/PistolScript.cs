using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject FirePoint;
    public GameObject AmmoBar;
    public int magCapacity = 10;
    public int magAmt = 10;
    public float fireRate = 0f;
    public float timeToFire = 0f;

    void Update()
    {
        if (fireRate == 0 && magAmt > 0)
        {
            if (Input.GetMouseButtonDown (0))
            {
                Debug.Log("shot");
                Instantiate(bullet, FirePoint.transform.position, FirePoint.transform.rotation);
                magAmt--;
            }
        }else if (magAmt > 0)
        {
            if (Input.GetMouseButtonDown (0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1/fireRate;
                Debug.Log("shot");
                Instantiate(bullet, FirePoint.transform.position, FirePoint.transform.rotation);
                magAmt--;
            }
        }
        // if (Input.GetMouseButtonDown(0) && magAmt > 0)
        // {
        //     Debug.Log("shot");
        //     Instantiate(bullet, FirePoint.transform.position, FirePoint.transform.rotation);
        //     magAmt--;
        // }
        if (Input.GetKeyDown(KeyCode.R) && magAmt < magCapacity)
        {
            magAmt = magCapacity;
            timeToFire = 0;
        }
        float Percentage = (float)magAmt/magCapacity;
        AmmoBar.transform.localScale = new Vector2(Percentage, AmmoBar.transform.localScale.y);
    }
}
