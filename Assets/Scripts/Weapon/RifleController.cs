using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RifleController : MonoBehaviour {

    bool isShooting;
    public bool hasAmmo;

    public Transform bulletFirePoint;
    public GameObject bulletObject;

    public PlayerController playerController;
    public UIController UI;


    BulletController BulletController;
    AudioSource[] audiosources;

    float counter;

    Weapon rifle;
    private void Awake()
    {
        rifle = new Weapon(
           GameConstants.RIFLE_GUN_NAME,
           GameConstants.RIFLE_AMMO,
           GameConstants.RIFLE_AMMO_IN_MAG,
           GameConstants.RIFLE_AMMO_PER_MAG,
           GameConstants.RIFLE_SECONDS_PER_AMMO
           );
        hasAmmo = true;
    }

    void Start () {
        audiosources = GetComponents<AudioSource>();
        BulletController = GetComponent<BulletController>();
        BulletController.CreateBullet(GameConstants.RIFLE_BULLET_VELOCITY, GameConstants.RIFLE_BULLET_DAMAGE, bulletObject);

        counter = 0;
    }

    void Update () {
        counter += Time.deltaTime;
        if (Input.GetMouseButton(0) && !isShooting)
        {
            Fire();
        }
        else if (Input.GetKey(KeyCode.R) && !isShooting && rifle.AmmoInMag != rifle.AmmoPerMag && rifle.Ammo != 0)
        {
            rifle.Reload();
            playerController.audiosources[3].Play();
        }
        else if (rifle.AmmoInMag == 0 && rifle.Ammo == 0 && counter >= 0.2f)
        {
            hasAmmo = false;
            playerController.DropWeapon(GameConstants.RIFLE_GUN_NAME);
        }
        UI.UpdateAmmo("ammo " + rifle.AmmoInMag + "/" + rifle.Ammo);
    }

    public void Fire()
    {
        if(rifle.AmmoInMag >= 1)
        {
            if(counter >= GameConstants.RIFLE_SECONDS_PER_AMMO)
            {
                isShooting = true;
                audiosources[0].Play();
                rifle.Fire();
                BulletController.Fire(bulletFirePoint);
                counter = 0;
                isShooting = false;
            }
        }
    }
    public void FillUp()
    {
        rifle.FillUp();
        hasAmmo = true;
    }
}
