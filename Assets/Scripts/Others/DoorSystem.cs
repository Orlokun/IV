using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{

    public bool puertaSimple;

    public static void OpenDoor(int doorID)
    {
        //Pseudo codigo
        //Animation.open(doors[doorID]);

        Destroy(GameManager._doors[doorID]);
    }

    void CloseDoor(int doorID)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (puertaSimple)
        {
            Destroy(gameObject);
        }
        else
        {

        }
    }
}
