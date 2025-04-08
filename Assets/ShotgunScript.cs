using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject FirePoint1;
    public GameObject AmmoBar1;
    public GameObject FirePoint2;
    public GameObject AmmoBar2;
    public int magCapacity = 2;
    public int magAmt = 2;
    public float devAng1 = 10;
    public float devAng2 = -10;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && magAmt > 0)
        {
            switch(magAmt)
            {
                case 2:
                {
                    Quaternion rotation1 = FirePoint1.transform.rotation * Quaternion.Euler(0, 0, devAng1);
                    Quaternion rotation2 = FirePoint1.transform.rotation * Quaternion.Euler(0, 0, devAng2);
                    GameObject.Instantiate(bullet, FirePoint1.transform.position, FirePoint1.transform.rotation);
                    GameObject.Instantiate(bullet, FirePoint1.transform.position, rotation1);
                    GameObject.Instantiate(bullet, FirePoint1.transform.position, rotation2);
                    magAmt--;
                    break;
                }
                case 1:
                {
                    Quaternion rotation1 = FirePoint2.transform.rotation * Quaternion.Euler(0, 0, devAng1);
                    Quaternion rotation2 = FirePoint2.transform.rotation * Quaternion.Euler(0, 0, devAng2);
                    GameObject.Instantiate(bullet, FirePoint2.transform.position, FirePoint2.transform.rotation);
                    GameObject.Instantiate(bullet, FirePoint2.transform.position, rotation1);
                    GameObject.Instantiate(bullet, FirePoint2.transform.position, rotation2);
                    magAmt--;
                    break;                    
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && magAmt < magCapacity)
        {
            magAmt = magCapacity;
        }
        switch(magAmt)
        {
            case 2:
            {
                AmmoBar1.transform.localScale = new Vector2(1, AmmoBar1.transform.localScale.y);
                AmmoBar2.transform.localScale = new Vector2(1, AmmoBar2.transform.localScale.y);
                break;
            }
            case 1:
            {
                AmmoBar1.transform.localScale = new Vector2(0, AmmoBar1.transform.localScale.y);               
                break;
            }
            case 0:
            {
                AmmoBar1.transform.localScale = new Vector2(0, AmmoBar1.transform.localScale.y);
                AmmoBar2.transform.localScale = new Vector2(0, AmmoBar2.transform.localScale.y);
                break;
            }
        }
    }
}
