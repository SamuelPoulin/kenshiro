using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionArrow : MonoBehaviour {

    Camera playerCamera;
    CameraRotation playerCameraRotation;

    Color color;
    Image detectionArrow;

    public Transform Ennemy;
    public Transform Player;

    Vector3 linkingVector;

    AiController AI;

    float opacity;
    public float Opacity
    {
        get { return opacity; }
        set
        {
            if (value >= 0)
            {
                opacity = value;
            }
        }
    }

	void Start () {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        playerCameraRotation = playerCamera.GetComponent<CameraRotation>();
        Player = playerCamera.transform.root;
        Ennemy = this.transform.root;
        AI = Ennemy.GetComponent<AiController>();
        this.transform.SetParent(GameObject.Find("UI").transform);
        detectionArrow = GetComponent<Image>();

        detectionArrow.rectTransform.sizeDelta = new Vector2(Screen.height, Screen.height);

        transform.localPosition = Vector3.zero;
        Opacity = 100;

        AI.data.Arrow = this.gameObject;
	}
	
	void Update () {

        Opacity = AI.data.spotting;

        this.transform.eulerAngles = new Vector3(0, 0, CalculateAngle());
		if (Opacity != detectionArrow.color.a)
        {
            if (AI.data.playerIsDetected)
            {
                detectionArrow.color = Color.red;
            }

            else
            {
                detectionArrow.color = Color.white;
                ImageTools.SetOpacity(Opacity, detectionArrow);
            }


            //ImageTools.SetOpacity(Opacity, detectionArrow);
        }
        if (Player.GetComponent<PlayerController>().IsDead)
        {
            Destroy(this.gameObject);
        }
	}

    public float CalculateAngle()
    {
        linkingVector = Ennemy.position - Player.position;
        return playerCameraRotation.x - Vector2.SignedAngle(new Vector2(linkingVector.x,linkingVector.z), Vector2.up);
    }
}
