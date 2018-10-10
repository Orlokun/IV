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
        switch (Player.weaponType)
        {
            case "Basic Blaster":
                msBeetwenShoots = 30;
                break;
            case "Minigun":
                msBeetwenShoots = 10;
                break;

            default:
                msBeetwenShoots = 100;
                break;
        }

        if (Time.time > nextShot)
        {
            AudioManager.BlasterPium();
            nextShot = Time.time + msBeetwenShoots / 100;
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Projectile newProjectile = (Projectile)Instantiate(projectile, muzzle.position, muzzle.rotation);
            newProjectile.SetSpeed(target, muzzleVelocity);
            newProjectile.SetLifeTime(projectile, 3);
            // :)
            Player.cbullets = Player.cbullets - 1;
            Debug.Log("Balas: " + Player.cbullets.ToString() + "/" + Player.bullets.ToString());
        }
    }

    public void Reload()
    {
        //AudioManager.BlasterReload(); Minigun, etc
        if (Player.charges > 0)
        {
            Player.charges = Player.charges - 1;
            Player.cbullets = Player.bullets;
        }
    }
}
