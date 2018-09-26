//Librerías
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Requiriendo componente en el GameObject
[RequireComponent (typeof(PlayerController))]
[RequireComponent(typeof(GunController))]


public class Player : MonoBehaviour
{
    //Variables Globales

    Camera viewCamera;
    public float moveSpeed;
    PlayerController pController;
    GunController gController;

    // Use this for initialization
    void Start()
    {
        pController = GetComponent<PlayerController>();
        gController = FindObjectOfType<GunController>();
        viewCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerShooting();
    }

    private void PlayerMovement()
    {
        Vector2 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);       //Defino un Vector3 en base a si se está apretando una tecla. 
        Vector2 moveVelocity = moveInput.normalized * moveSpeed;
        pController.Move(moveVelocity);

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.back, Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector2 point = ray.GetPoint(rayDistance);
            pController.LookAt(point);
        }
    }

    private void PlayerShooting()
    {
        if(Input.GetMouseButton(0))
        {
            gController.Shoot();
        }
    }
}
