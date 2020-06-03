using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet{

    int damage;
    public int Damage
    {
        get { return damage; }
        private set
        {
            if (value >= 0 && value <= 100)
            {
                damage = value;
            }
        }
    }

    Vector3 velocity;
    public Vector3 Velocity
    {
        get { return velocity; }
        private set
        {
            if (value.z > 0)
            {
                velocity = value;
            }
        }
    }

    GameObject bulletObject;
    public GameObject BulletObject { get; private set; }

    public Bullet(float velocity, int damage, GameObject bullet)
    {
        Velocity = new Vector3(0, 0, velocity);
        Damage = damage;
        BulletObject = bullet;
    }
}
