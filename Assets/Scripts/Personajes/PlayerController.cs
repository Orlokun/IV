using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private Vector2 pVelocity;
    // Use this for initialization


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + pVelocity * Time.fixedDeltaTime);
    }

    public void Move(Vector2 velocity)
    {
        pVelocity = velocity;
    }

    public void LookAt(Vector2 point)
    {
        Vector2 diff = new Vector2(transform.position.x, transform.position.y) - point;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }
}
