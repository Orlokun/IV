using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static void PickUpItem(string Type, GameObject Object)
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

            case "Canion":
                Player.weaponType = "Canion";
                Destroy(Object);
                break;

            case "Cafe":
                Player.cHP = Player.cHP + 10;
                Destroy(Object);
                break;

            case "BolsaCafe":
                Player.cHP = 100;
                Destroy(Object);
                break;

            case "Money":
                Player.cMoney = Player.cMoney + (10 * Random.Range(1, 4));
                Destroy(Object);
                break;
            default:
                break;
        }
    }
}
