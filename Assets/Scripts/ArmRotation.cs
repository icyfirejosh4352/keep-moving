using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public int rotationOffset = 0;

    public float rotZ;
    // TODO: add the recoil mechanic. (remember to use trignometry)
    void Update()
    {
        //ripped straight out of brackey's video.
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize(); 

        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
    }
}