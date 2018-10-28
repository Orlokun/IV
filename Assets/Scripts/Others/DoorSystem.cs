using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{

    public List<GameObject> doors;
	
	// Update is called once per frame
	void Update ()
    {
		switch(Player.kEnemies)
        {
            case 5:
                OpenDoor(1);
                break;
            case 10:
                OpenDoor(2);
                break;
            case 15:
                OpenDoor(3);
                break;
            default:
                break;
        }
	}

    void OpenDoor(int doorID)
    {

    }

    void CloseDoor(int doorID)
    {

    }
}
