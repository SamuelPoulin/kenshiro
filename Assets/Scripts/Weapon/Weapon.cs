using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon {

    string name;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            if (value != null)
            {
                name = value;
            }
            else
                name = "Nom non spécifié";
        }
    }

    int ammo;
    public int Ammo
    {
        get
        {
            return ammo;
        }
        set
        {
            if (value >= 0)
            {
                ammo = value;
            }
            else
                ammo = 0;
        }
    }

    int ammoInMag;
    public int AmmoInMag
    {
        get
        {
            return ammoInMag;
        }
        set
        {
            if (value >= 0)
            {
                ammoInMag = value;
            }
            else
                ammoInMag = 0;
        }
    }

    int ammoPerMag;
    public int AmmoPerMag
    {
        get
        {
            return ammoPerMag;
        }
        set
        {
            if (value >= 0)
            {
                ammoPerMag = value;
            }
            else
                ammoPerMag = 99;
        }
    }

    float secondPerAmmo;
    public float SecondPerAmmo
    {
        get
        {
            return secondPerAmmo;
        }
        set
        {
            if (value > 0)
            {
                secondPerAmmo = value;
            }
            else
                secondPerAmmo = 99;
        }
    }
    public Weapon(string name,int ammo,int ammoInMag, int ammoPerMag, float SecondPerAmmo)
    {
        Name = name;
        Ammo = ammo;
        AmmoInMag = ammoInMag;
        AmmoPerMag = ammoPerMag;
        SecondPerAmmo = secondPerAmmo;
    }
	
    public void Fire()
    {
        if (AmmoInMag > 0)
        {
            AmmoInMag -= 1;
        }
    }

    int ammoToSubstract;
    public void Reload()
    {
        if(Ammo > 0)
        {
            ammoToSubstract = AmmoPerMag - AmmoInMag;
            if (Ammo >= AmmoPerMag)
            {
                AmmoInMag = AmmoPerMag;
                Ammo -= ammoToSubstract;
            }
            else if(Ammo >= ammoToSubstract)
            {
                AmmoInMag += ammoToSubstract;
                Ammo -= ammoToSubstract;
            }
            else
            {
                AmmoInMag += Ammo;
                Ammo = 0;
            }
        }
            
    }

    public void FillUp()
    {
        Ammo = AmmoPerMag * 2;
    }
}
