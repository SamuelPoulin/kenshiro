using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    PlayerHealthbar health;
    public Animator animator;
    public RuntimeAnimatorController noWeaponController;
    public RuntimeAnimatorController hasGunController;
    public UIController UI;

    public GameObject dyingBody;
    public GameObject playerBody;
    Renderer[] characterRenderers;

    public List<GameObject> Weapons;
    public string activeWeapon;
    public RailgunController Railgun;
    public RifleController Rifle;

    public AudioSource[] audiosources;

    bool isWeaponActive;

    bool isDead;
    public bool IsDead { get; set; }

    void Start() {

        CreateWeaponList();
        
        health = GetComponent<PlayerHealthbar>();
        characterRenderers = playerBody.GetComponentsInChildren<Renderer>();
        audiosources = GetComponents<AudioSource>();

        isDead = false;
        isWeaponActive = false;
        animator.runtimeAnimatorController = noWeaponController;
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Railgun.hasAmmo)
                SwitchWeapons(GameConstants.RAILGUN_GUN_NAME);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Rifle.hasAmmo)
                SwitchWeapons(GameConstants.RIFLE_GUN_NAME);
        }

        if (isWeaponActive)
        {
            animator.runtimeAnimatorController = hasGunController;
            isWeaponActive = true;
        }
        if (!isWeaponActive)
        {
            animator.runtimeAnimatorController = noWeaponController;
            isWeaponActive = false;
            UI.UpdateAmmo("");
        }
    }

    void CreateWeaponList()
    {
        Weapons = new List<GameObject>
        {
            Railgun.gameObject,
            Rifle.gameObject
        };
    }


    public void PowerUpPickup(string name)
    {
        if(name.Contains("RailgunGroundPowerUp"))
        {
            SwitchWeapons(GameConstants.RAILGUN_GUN_NAME);
            Railgun.FillUp();
        }
        if (name.Contains("RifleGroundPowerUp"))
        {
            SwitchWeapons(GameConstants.RIFLE_GUN_NAME);
            Rifle.FillUp();
        }
        if (name.Contains("MedpackGroundPowerUp"))
        {
            health.TakeHealing(100);
        }
    }

    public void DropWeapon(string name)
    {
        audiosources[2].Play();
        Weapons.Find(x => x.name == name).SetActive(false);

        isWeaponActive = false;
    }

    private void SwitchWeapons(string name)
    {
        foreach (GameObject weapon in Weapons)
        {
            if(weapon.name == name)
            {
                weapon.SetActive(true);
                activeWeapon = weapon.name;
                isWeaponActive = true;
            }
            else
            {
                weapon.SetActive(false);
            }
        }
    }

    public void Death()
    {
        isDead = true;
        foreach (Renderer r in characterRenderers)
        {
            r.enabled = false;
        }
        SwitchWeapons(null);
        dyingBody.SetActive(true);
    }
}
