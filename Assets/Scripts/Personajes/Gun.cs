using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform muzzle;
    public Projectile projectile;
    public float msBeetwenShoots = 100;
    public float muzzleVelocity = 35;
    public float speed = 5;

    float nextShot = 0;

    public void Shoot()
    {
        if (Time.time > nextShot)
        {
            nextShot = Time.time + msBeetwenShoots / 100;
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Projectile newProjectile = (Projectile)Instantiate(projectile, muzzle.position, muzzle.rotation);
            newProjectile.SetSpeed(target, muzzleVelocity);
        }
    }
}
