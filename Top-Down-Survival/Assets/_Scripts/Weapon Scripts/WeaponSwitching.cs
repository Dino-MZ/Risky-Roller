using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] int selectedWeapon = 0;

    [SerializeField] private Transform currentWeaponPos, equippedWeaponPos;

    private void Start()
    {
        selectWeapon();
    }

    void Update()
    {
        int previousWeapon = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKeyDown(KeyCode.E))
        {
            if (selectedWeapon >= transform.childCount-1)
            { 
                selectedWeapon = 0; 
            }
            else
            {
                selectedWeapon++;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown(KeyCode.Q))
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount-1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if(previousWeapon != selectedWeapon)
        {
            selectWeapon();
        }
    }

    void selectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            // Selected Weapon
            if(i == selectedWeapon && weapon.GetComponent<Gun>().hasUnlockedGun)
            {
                weapon.gameObject.GetComponent<Gun>().gunSprite.color = new Color(1, 1, 1, 1f);
                weapon.gameObject.GetComponent<Gun>().isActive = true;
                weapon.position = currentWeaponPos.position;
            }
            else
            {
                weapon.gameObject.GetComponent<Gun>().gunSprite.color = new Color(1, 1, 1, 0.2f);
                weapon.gameObject.GetComponent<Gun>().isActive = false;
                weapon.position = equippedWeaponPos.position + (Vector3.up * i * 0.5f);
            }
            i++;
        }
    }
}
