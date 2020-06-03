using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupController : MonoBehaviour {

    PlayerController playerController;

    const float TRANSLATION_HEIGHT = 0.5f;
    const float TRANSLATION_SPEED = 1f;
    const int ROTATION_SPEED = 50;

    Vector3 rotationVector;
    Vector3 initialPosition;
    new Transform transform;

    Renderer[] renderers;


    float counter;

    AudioClip pickupSound;

    GameObject playerCharacter;

    void Start () {
        playerCharacter = GameObject.Find("Liam");
        renderers = GetComponentsInChildren<Renderer>();
        transform = GetComponent<Transform>();
        pickupSound = Resources.Load<AudioClip>("Sounds/GunPickup");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        initialPosition = transform.position;
        counter = 0;
    }

    void Update() {
        transform.Rotate(new Vector3(0, Time.deltaTime * ROTATION_SPEED, 0));
        transform.position = new Vector3(initialPosition.x, initialPosition.y + Mathf.Sin(counter) * TRANSLATION_HEIGHT, initialPosition.z);

        counter += Time.deltaTime * TRANSLATION_SPEED;
        counter %= 2 * Mathf.PI;

    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Player")
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<AudioSource>().PlayOneShot(pickupSound);
            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }
            playerController.PowerUpPickup(this.gameObject.name);
            Destroy(this.gameObject, pickupSound.length);
        }
    }
}
