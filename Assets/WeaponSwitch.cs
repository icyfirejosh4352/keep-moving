using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject pistolObj;
    public GameObject shotgunObj;
    public GameObject SMGObj;
    public enum Weapons
    {
        pistol,
        shotgun,
        SMG
    }
    public Weapons currentEquipped;
    void Start()
    {
        currentEquipped = Weapons.pistol;
    }

    void Update()
    {
        switch(currentEquipped)
        {
            case Weapons.pistol:
            {
                pistolObj.SetActive(true);
                shotgunObj.SetActive(false);
                SMGObj.SetActive(false);
                break;               
            }
            case Weapons.shotgun:
            {
                pistolObj.SetActive(false);
                shotgunObj.SetActive(true);
                SMGObj.SetActive(false);                
                break;
            }
            case Weapons.SMG:
            {
                pistolObj.SetActive(false);
                shotgunObj.SetActive(false);
                SMGObj.SetActive(true);
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentEquipped = Weapons.pistol;
        }else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentEquipped = Weapons.shotgun;
        }else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentEquipped = Weapons.SMG;
        }
    }
}
