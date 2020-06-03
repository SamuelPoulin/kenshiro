using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    bool needFPS;
    bool cursorIsLocked;
    bool altCamIsEnabled;

    public Text UIAmmoText;
    public Image crosshair;
    public Image bloodsplatter;
    public FPS text;

    public Material[] ennemyMaterial;
    public Camera mainCam;
    public Camera altCam;
    public Shader testShader;

    PlayerController playerController;

    

    // Use this for initialization
    void Start () {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        needFPS = false;

        cursorIsLocked = true;
        Cursor.lockState = CursorLockMode.Locked;

        altCam.SetReplacementShader(testShader, "RenderType");
    }
	
	// Update is called once per frame
	void Update () {
        DisplayCrosshair();

        if (Input.GetKey(KeyCode.Backspace))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleFPS();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            ToggleCursor();
        }
        if (Input.GetKey(KeyCode.L))
        {
            ToggleCamera();
        }
    }

    public void DisplayHealthBar(float health)
    {
        ImageTools.SetOpacity(100 - health, bloodsplatter);
    }

    void DisplayCrosshair()
    {
        if(playerController.IsDead)
        {
            crosshair.gameObject.SetActive(false);
        }
    }

    public void UpdateAmmo(string ammo)
    {
        UIAmmoText.text = ammo;
    }

    void ToggleFPS()
    {
        if (needFPS)
        {
            text.gameObject.SetActive(false);
        }
        else
        {
            text.gameObject.SetActive(true);
        }
        needFPS = !needFPS;
    }

    void ToggleCursor()
    {
        if (cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        cursorIsLocked = !cursorIsLocked;
    }

    void ToggleCamera()
    {
        if (altCamIsEnabled)
        {
            for (int i = 0; i < ennemyMaterial.Length; ++i)
            {
                ennemyMaterial[i].SetOverrideTag("RenderType", "Ennemy");
            }
            altCam.gameObject.SetActive(true);

            //mainCam.gameObject.SetActive(false);
        }

        else
        {
            for (int i = 0; i < ennemyMaterial.Length; ++i)
            {
                ennemyMaterial[i].SetOverrideTag("RenderType", "Opaque");
            }
            altCam.gameObject.SetActive(false);
            //mainCam.gameObject.SetActive(true);
        }

        altCamIsEnabled = !altCamIsEnabled;
    }
}
