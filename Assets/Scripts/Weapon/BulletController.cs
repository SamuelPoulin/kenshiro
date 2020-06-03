using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public Bullet bullet;

    GameObject Clone;

    public void CreateBullet(float velocity, int damage, GameObject bulletObject)
    {
        bullet = new Bullet(velocity, damage, bulletObject);
    }

    public void Fire(Transform bulletFirePoint)
    {
        Clone = Instantiate(bullet.BulletObject, bulletFirePoint.position, bulletFirePoint.rotation);
        Clone.SetActive(true);
        Clone.GetComponent<Rigidbody>().velocity = bulletFirePoint.rotation * bullet.Velocity;
    }
}
