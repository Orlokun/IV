using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamiLord : MonoBehaviour
{
    public Sprite normal;
    public Sprite angry;

    public Transform target;
    public float speed = 3f;

    public int hp = 100;
    public int cHP = 100;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //rotate to look at the player
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation


        //move towards the player
        if (Vector3.Distance(transform.position, target.position) > 1f)
        {//move if distance from target is greater than 1
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        if(cHP < 50)
        {
            GetComponent<SpriteRenderer>().sprite = angry;
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
            cHP = cHP - damage;
        }
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
}
