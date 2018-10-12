using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static int dificultad; /* 0, 1, 2 */

	// Use this for initialization
	void Start ()
    {
        /*HideCursor();*/
    }

    public static void HideCursor()
    {
        /*Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;*/
    }

    public static void ShowCursor()
    {
        /*Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;*/
    }
}
