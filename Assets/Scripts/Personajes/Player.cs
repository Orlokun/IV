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

    //Variables Estaticas, para llamarlas desde otros lados sin tanta cosa :D
    public static string weaponType;
    public static int maxHP = 100;
    public static int cHP = 100;
    public static int bullets;
    public static int cbullets;
    public static int charges;

    // Use this for initialization
    void Start()
    {
        weaponType = "Minigun";

        pController = GetComponent<PlayerController>();
        gController = FindObjectOfType<GunController>();
        viewCamera = FindObjectOfType<Camera>();

        switch (weaponType)
        {
            case "Basic Blaster":
                bullets = 30;
                cbullets = bullets;
                charges = 4; // 4 cargadores
                break;
            case "Minigun":
                bullets = 60;
                cbullets = bullets;
                charges = 10; // 4 cargadores
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerShooting();
        PlayerReloading();
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
        if (PauseMenu.gamePaused)
        {

        }
        else
        {
            if (Input.GetButton("Fire1"))
            {
                /* HOLA SOY UN COMENTARIO
                 * 
                 * Me da lata usar Switch eso chao <3
                 */
                if (cbullets > 0)
                {
                    gController.Shoot();
                }

                else if (cbullets < 0)
                {
                    cbullets = 0;
                }

                else if (cbullets == 0)
                {
                    Debug.Log("NO TIENES BALAS :c");
                }

                else
                {
                    Debug.Log("NO TIENES BALAS :c");
                }
            }
        }
    }

    private void PlayerReloading()
    {
        if (PauseMenu.gamePaused)
        {

        }
        else
        {
            if (Input.GetButtonDown("Reload"))
            {
                if (cbullets != bullets)
                {
                    /* HOLA SOY UN COMENTARIO
                     * 
                     * Me da lata usar Switch eso chao <3
                     */
                    if (charges > 0)
                    {
                        gController.Reload();
                    }

                    else if (charges < 0)
                    {
                        charges = 0;
                    }

                    else if (charges == 0)
                    {
                        Debug.Log("NO TIENES Cargadores :c");
                    }

                    else
                    {
                        Debug.Log("NO TIENES Cargadores :c");
                    }
                }
            }
        }
    }
}
