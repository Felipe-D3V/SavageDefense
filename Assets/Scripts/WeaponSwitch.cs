using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject MeleeWeapon;
    public GameObject RangedWeapon;
    // Start is called before the first frame update
    void Start()
    {
        RangedWeapon.SetActive(false);
        MeleeWeapon.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            RangedWeapon.SetActive(false);
            MeleeWeapon.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RangedWeapon.SetActive(true);
            MeleeWeapon.SetActive(false);
        }
    }
}
