using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    private Gun currentGun;
    public Gun startingGun;
    public Transform weaponHolder;


    // Use this for initialization
	void Start () {

        if (startingGun)
        {
            EquipGun(startingGun);
        }
	}

    public void EquipGun(Gun incomingGun)
    {
        if (currentGun != null)
        {
            Destroy(currentGun.gameObject);
        }
        currentGun = (Gun)Instantiate(incomingGun, weaponHolder);
        currentGun.transform.parent = weaponHolder;
    }

    public void Shoot()
    {
        if (currentGun != null)
        {
            currentGun.Shoot();
        }
    }
}
