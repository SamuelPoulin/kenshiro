using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    PlayerController playerController;
    public Animator animator;
    public Transform head;

    public float x = 0;
    public float y = 0;

    public float roty;
    public float rotx;

    Vector3 headOffset;

    private void Awake()
    {
        Application.targetFrameRate = 1000;
        QualitySettings.vSyncCount = 0;
        Cursor.visible = false;
    }

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        headOffset = new Vector3(0.15f, 0.4f, 0.15f);
        x = 90;
    }
    void LateUpdate()
    {
        if (!playerController.IsDead)
        {
            x += Input.GetAxis("Mouse X");
            y -= Input.GetAxis("Mouse Y");

            if (y >= 70)
                y = 70;
            if (y <= -70)
                y = -70;

            transform.rotation = Quaternion.Euler(new Vector3(y, x, 0));
            transform.position = new Vector3(head.position.x + Mathf.Sin(Mathf.Deg2Rad * x)* headOffset.x, head.position.y + 0.25f, head.position.z + Mathf.Cos(Mathf.Deg2Rad * x)* headOffset.z);
        }
    }
}