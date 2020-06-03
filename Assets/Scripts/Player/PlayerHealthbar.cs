using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour {

    bool isSoundDone;

    UIController ui;
    PlayerController playerController;

    public Image bloodsplatter;

    float counterRestore;
    float counterJustGotHurt;
    bool justGotHurt;

    public AudioClip[] hurtSounds;
    

	void Start () {
        playerController = GetComponent<PlayerController>();
        ui = GameObject.Find("UI").GetComponent<UIController>();

        Health = 100;
        counterRestore = 0;
        counterJustGotHurt = 0;

        SetVolume();

        justGotHurt = false;
        isSoundDone = true;
	}
	
	void Update () {

		if(Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeHealing(10);
        }

        if(justGotHurt == true)
        {
            counterJustGotHurt += Time.deltaTime;
        }
        if (Health < 100 && Health > 0)
        {
            if (justGotHurt == false)
            Health += 5 * Time.deltaTime;
        }
        if(counterJustGotHurt > 10)
        {
            justGotHurt = false;
            counterJustGotHurt = 0;
        }

        ui.DisplayHealthBar(Health);
    }

    float health;
    public float Health
    {
        get { return health; }
        private set
        {
            if (value >= 0 && value <= 100)
            {
                health = value;
                ;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("AiProjectile"))
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!playerController.IsDead)
        {
            if (Health - damage > 0)
            {
                justGotHurt = true;
                Health -= damage;
                StartCoroutine(PlayHurtSound());
                SetVolume();
            }
            else
            {
                Health = 0;
                playerController.Death();
                playerController.IsDead = true;
            }
        }
    }

    public void TakeHealing(int healing)
    {
        if (!playerController.IsDead)
        {
            if (Health + healing > 100)
            {
                Health = 100;
                SetVolume();
            }
            else
            {
                Health += healing;
                SetVolume();
            }
        }
    }

    void SetVolume()
    {
        playerController.audiosources[5].volume = (1 - Health/100);
    }
    IEnumerator PlayHurtSound()
    {
        if(isSoundDone)
        {
            isSoundDone = false;
            playerController.audiosources[4].clip = hurtSounds[Random.Range(0, hurtSounds.Length)];
            playerController.audiosources[4].Play();
            yield return new WaitForSeconds(hurtSounds[Random.Range(0, hurtSounds.Length)].length);
            isSoundDone = true;
        }
    }
}
