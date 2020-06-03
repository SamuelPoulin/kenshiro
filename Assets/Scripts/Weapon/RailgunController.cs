using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RailgunController : MonoBehaviour {

    bool isShooting;
    public bool hasAmmo;

    public Transform bulletFirePoint;
    public GameObject bulletObject;

    public Text ammoText;
    public PlayerController playerController;
    public UIController UI;

    BulletController BulletController;
    AudioSource[] audiosources;

    Weapon railgun;

    private void Awake()
    {
        railgun = new Weapon(
           GameConstants.RAILGUN_GUN_NAME,
           GameConstants.RAILGUN_AMMO,
           GameConstants.RAILGUN_AMMO_IN_MAG,
           GameConstants.RAILGUN_AMMO_PER_MAG,
           GameConstants.RAILGUN_SECONDS_PER_AMMO
           );
        hasAmmo = true;
    }
    void Start () {
        audiosources = GetComponents<AudioSource>();
        BulletController = GetComponent<BulletController>();
        BulletController.CreateBullet(GameConstants.RAILGUN_BULLET_VELOCITY, GameConstants.RAILGUN_BULLET_DAMAGE, bulletObject);
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0) && !isShooting)
        {
            StartCoroutine(Fire());
        }
        if (Input.GetKey(KeyCode.R) && !isShooting && railgun.AmmoInMag != railgun.AmmoPerMag)
        {
            railgun.Reload();
            playerController.audiosources[3].Play();
        }

        ammoText.text = railgun.AmmoInMag.ToString();
        if(!audiosources[0].isPlaying)
        {
            audiosources[0].Play();
        }
        UI.UpdateAmmo("ammo " + railgun.AmmoInMag + "/" + railgun.Ammo);
    }

    IEnumerator Fire()
    {
        if(railgun.AmmoInMag >= 6)
        {
            isShooting = true;
            audiosources[1].Play();
            yield return new WaitForSeconds(1.15f);
            railgun.Fire();
            BulletController.Fire(bulletFirePoint);

            for (int i = 0; i < 5; ++i)
            {
                yield return new WaitForSeconds(GameConstants.RAILGUN_SECONDS_PER_AMMO);
                railgun.Fire();
                BulletController.Fire(bulletFirePoint);
            }
            isShooting = false;
            if(railgun.Ammo == 0 && railgun.AmmoInMag == 0)
            {
                yield return new WaitForSeconds(0.2f);
                hasAmmo = false;
                playerController.DropWeapon(GameConstants.RAILGUN_GUN_NAME);
            }
        }
    }

    public void FillUp()
    {
        railgun.FillUp();
        hasAmmo = true;
    }
}
