using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    private Vector2 direction;


    public void SetSpeed(Vector2 incomingTarget, float incomingSpeed)
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = incomingSpeed;
        float myX = transform.position.x;
        float myY = transform.position.y;
        direction = incomingTarget;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.velocity = transform.up * 10;
    }
}