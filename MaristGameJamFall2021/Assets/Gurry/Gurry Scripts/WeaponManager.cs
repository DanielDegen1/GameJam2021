using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    PlayerInput playerInput;
    private int selectedWeapon;
    public GameObject[] Weapons;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.Swap)
        {
            
                selectedWeapon++;
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
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
