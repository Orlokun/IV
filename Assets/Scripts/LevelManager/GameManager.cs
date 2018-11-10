using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<GameObject> doors;
    public static List<GameObject> _doors;
    public static int dificultad; /* 0, 1, 2 */

	// Use this for initialization
	void Start ()
    {
        /*HideCursor();*/
        _doors = doors;

    }

    void Update()
    {
        switch (Player.kEnemies)
        {
            case 5:
                DoorSystem.OpenDoor(0);
                break;
            case 10:
                DoorSystem.OpenDoor(1);
                break;
            case 15:
                DoorSystem.OpenDoor(2);
                break;
            default:
                break;
        }
    }

    public static void HideCursor()
    {
        /*Cursor.visible = false; c:
        Cursor.lockState = CursorLockMode.None;*/
    }

    public static void ShowCursor()
    {
        /*Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;*/
    }
}
