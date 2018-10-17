using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static void pickUpItem(string Type, GameObject Object)
    {
        switch (Type)
        {
            case "Ammo":
                var getRandom = Random.Range(1, 3);
                Player.charges = Player.charges + getRandom;
                Destroy(Object);
                break;
            case "Blaster":
                Player.weaponType = "Basic Blaster";
                Destroy(Object);
                break;
            case "Minigun":
                Player.weaponType = "Minigun";
                Destroy(Object);
                break;
            default:
                break;
        }
    }
}
