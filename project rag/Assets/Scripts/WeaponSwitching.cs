using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    public PlayerShoot playerShoot;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousthing = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }

        if (previousthing != selectedWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                playerShoot.selectedWeaponObject = weapon.gameObject;
                playerShoot.selectedWeapon = weapon.gameObject.transform.GetComponent<Weapon>();
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}