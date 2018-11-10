﻿using System.Collections;
using UnityEngine;

public class CamiLord : MonoBehaviour
{
    private Rigidbody2D myRbd;
    public Transform target;
    public float speed = 3f;

    public int hp = 100;
    public int cHP = 100;

    public bool isRed = false;
    public Sprite isSprite;

    [Header("Sprites")]
    public Sprite camilord_Normal;
    public Sprite camilord_Angry;
    public Sprite camilord_Normal_Hitted;
    public Sprite camilord_OpenMouth;


    // Use this for initialization
    void Start()
    {
        myRbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate to look at the player
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation


        //move towards the player
        if (Vector3.Distance(transform.position, target.position) > 1f && Vector3.Distance(transform.position, target.position) < 15f)
        {//move if distance from target is greater than 1
            AnimationSwitch(camilord_OpenMouth);
            ShootPlayer(target.position);
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        isSprite = camilord_Normal;

        if (cHP < 50)
        {
            GetComponent<SpriteRenderer>().sprite = camilord_Angry;
            isSprite = camilord_Angry;
        }

    }

    private void Damage(int damage)
    {
        if(cHP < 0)
        {
            Destroy(gameObject);
        }

        else
        {
            if (!isRed)
            {
                StartCoroutine(BlinkToRed(camilord_Normal_Hitted, isSprite));
            }
            cHP = cHP - damage;
        }
    }

    IEnumerator BlinkToRed(Sprite sprite, Sprite spriteADevolver)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteADevolver;
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                Damage(1);
                break;
            default:
                break;
        }
    }

    private void ShootPlayer(Vector2 targetPlayer)
    {
        System.Random newRandom = new System.Random();
        int piezaDeAjedrez = 10;
        InstanciarPieza(piezaDeAjedrez);
    }

    private void InstanciarPieza(int pieza)
    {
        switch (pieza)
        {
            case 1:
                GameObject projectile = (GameObject)Instantiate(Resources.Load("ChessPiece/" + "pieza_" + pieza.ToString()));
                break;
            case 2:
                GameObject projectile1 = (GameObject)Instantiate(Resources.Load("ChessPiece/" + "pieza_" + pieza.ToString()));
                break;
            case 3:
                GameObject projectile2 = (GameObject)Instantiate(Resources.Load("ChessPiece/" + "pieza_" + pieza.ToString()));
                break;
            case 4:
                GameObject projectile3 = (GameObject)Instantiate(Resources.Load("ChessPiece/" + pieza.ToString()));
                break;
            case 5:
                GameObject projectile4 = (GameObject)Instantiate(Resources.Load("ChessPiece/" + "pieza_" + pieza.ToString()));
                break;
            case 6:
                GameObject projectile5 = (GameObject)Instantiate(Resources.Load("ChessPiece/" + "pieza_" + pieza.ToString()));
                break;
            case 7:
                GameObject projectile6 = (GameObject)Instantiate(Resources.Load("ChessPiece/" + "pieza_" + pieza.ToString()));
                break;
            case 8:
                GameObject projectile7 = (GameObject)Instantiate(Resources.Load("ChessPiece/" + "pieza_" + pieza.ToString()));
                break;
            case 9:
                GameObject projectile8 = (GameObject)Instantiate(Resources.Load("ChessPiece/" + "pieza_" + pieza.ToString()));
                break;
            case 10:
                GameObject projectile9 = (GameObject)Instantiate(Resources.Load("ChessPiece/" + "pieza_" + pieza.ToString()));
                Debug.Log("El objeto a instaciar es este: pieza " + pieza.ToString());
                break;
            case 11:
                GameObject projectile10 = (GameObject)Instantiate(Resources.Load("ChessPiece/" + "pieza_" + pieza.ToString()));
                break;
            case 12:
                GameObject projectile11 = (GameObject)Instantiate(Resources.Load("ChessPiece/" + "pieza_" + pieza.ToString()));
                break;
        }
    }
    private void AnimationSwitch(Sprite incomingSprite)
    {
        isSprite = incomingSprite;
    }

    private void OnGUI()
    {
                
    }
}
