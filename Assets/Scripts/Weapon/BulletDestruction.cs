using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestruction : MonoBehaviour {

    private void Start()
    {
        this.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    private void OnCollisionEnter()
    {
        Destroy(this.gameObject);
    }
}
