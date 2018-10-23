﻿//Librerías
using UnityEngine;

//Requiriendo componente en el GameObject
[RequireComponent (typeof(PlayerController))]
[RequireComponent(typeof(GunController))]


public class Player : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite Happy;
    public Sprite Angry;
    public Sprite Hitted;

    //Variables Globales

    [Header("Requisitos")]
    public float moveSpeed;
    Camera viewCamera;
    PlayerController pController;
    GunController gController;
    public Transform playerTransform;

    //Variables Estaticas, para llamarlas desde otros lados sin tanta cosa :D

    public static string weaponType;
    public static int maxHP; // Tuve que pasarlos a float porque la wea no se dividia bien. Chequear en Start los Parámetros.
    public static int cHP = maxHP;
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
                charges = 2; // 4 cargadores
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
        UpdateSprite();
        PlayerShooting();
        PlayerReloading();
        CheckHealthPoints();
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

    private void UpdateSprite()
    {
        /*if (Input.GetAxisRaw("Vertical") > 0.01)
        {
            GetComponent<SpriteRenderer>().sprite = upSprite;
        }
        else if (Input.GetAxisRaw("Vertical") < -0.01)
        {
            GetComponent<SpriteRenderer>().sprite = downSprite;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0.01)
        {
            GetComponent<SpriteRenderer>().sprite = rightSprite;
        }
        else if (Input.GetAxisRaw("Horizontal") < -0.01)
        {
            GetComponent<SpriteRenderer>().sprite = leftSprite;
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemigo":
                Damage(10);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ammo":
                Item.pickUpItem("Ammo", collision.gameObject);
                break;
            case "Blaster":
                Item.pickUpItem("Blaster", collision.gameObject);
                break;
            case "Minigun":
                Item.pickUpItem("Minigun", collision.gameObject);
                break;
            default:
                break;
        }
    }

    void Damage(int damage)
    {
        if(cHP <= 0)
        {
            cHP = 0;
            Die();
        }
        else if(cHP < 0)
        {
            cHP = 0;
            Die();
        }
        else if (cHP > 0)
        {
            cHP = cHP - damage;
        }
    }

    void CheckHealthPoints()
    {
        if (cHP <= 0)
        {
            cHP = 0;
            Die();
        }
        else if (cHP < 0)
        {
            cHP = 0;
            Die();
        }
    }

    void Die()
    {
        UIManager.ChangeStateGameObject("mainGUI", false);
        Time.timeScale = 0;
        PauseMenu.gamePaused = true;
        UIManager.ChangeStateGameObject("deathGUI", true);
    }
}
