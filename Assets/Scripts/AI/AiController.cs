using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour {

    public AiData data;

    public Vector3[] map;

    Camera AiCamera;

    public Material[] ennemyMaterial;
    //public Material testMaterial;

    public GameObject AiBullet;


    public GameObject DetectionArrow;

    GameObject myArrow;

    float fireTimer;

    AudioSource[] audio;

    RectTransform healthBarCanvas;
    //Vector2 healthBarSize;
    RectTransform healthBar;

    void Awake()
    {
        data = new AiData();
    }

    void Start () {

        fireTimer = 0;

        AiCamera = GetComponentInChildren<Camera>();


        myArrow = Instantiate(DetectionArrow, this.transform, false);

        audio = GetComponents<AudioSource>();

        healthBarCanvas = GetComponentInChildren<RectTransform>();
        //healthBarSize = healthBarCanvas.GetComponent<RectTransform>().sizeDelta;
        //healthBarSize = healthBarCanvas.sizeDelta;
        healthBar = healthBarCanvas.GetComponentInChildren<RectTransform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            data.TakeDamage(50);
        }

        
    }

    void Update () {

        healthBar.localScale = new Vector3(data.health / 100f,1,1);
        //healthBar.localPosition = new Vector3(0, 0, 0);
        //healthBar.anchoredPosition = new Vector2(0, 0);

        /*Debug.Log(healthBarSize.x);
        Debug.Log(healthBarSize.x * (data.health / 100f));*/

        if(data.currentPath != null)
        {


            /*try
            {
                data.currentDestinationIndex = data.currentPath[data.currentPathIndex];
                data.currentDestination = map[data.currentDestinationIndex];
            }
            catch
            {
                Debug.Log(data.currentPathIndex + " current path index");
                Debug.Log(data.currentDestinationIndex + " dest index");
            }*/

            data.currentDestinationIndex = data.currentPath[data.currentPathIndex];
            data.currentDestination = map[data.currentDestinationIndex];


            data.travelDirection = new Vector3(data.currentDestination.x - transform.position.x, 0, data.currentDestination.z - transform.position.z);
            data.travelDirection.Normalize();
            transform.position += data.travelDirection * Time.deltaTime * 25;
           

            if (data.IsAtCurrentDestination(transform.position))
            {
                data.lastVisitedIndex = data.currentDestinationIndex;

                data.currentPathIndex += data.loopIndexModifier;

                if (data.currentPathIndex == data.currentPath.Length || data.currentPathIndex == -1)
                {
                    if (data.loopPath)
                    {
                        data.loopIndexModifier = -data.loopIndexModifier;
                        data.currentPathIndex += data.loopIndexModifier;
                    }

                    else
                    {
                        data.currentPath = null;
                    }
                }

                
            }
        }

        if(data.playerIsDetected && fireTimer > 0.25f)
        {
            Bullet bullet = new Bullet(100, 10, AiBullet);

            GameObject Clone = Instantiate(AiBullet, AiCamera.transform.position, AiCamera.transform.rotation);
            Clone.SetActive(true);
            Clone.GetComponent<Rigidbody>().velocity = AiCamera.transform.rotation * bullet.Velocity;

            fireTimer = 0;

            audio[2].Play();
        }

        if (!data.wasSpotted && data.playerIsDetected)
        {
            audio[1].Play();
        }

        data.wasSpotted = data.playerIsDetected;

        fireTimer += Time.deltaTime;
	}

    public void UpdateCamera()
    {
        for (int i = 0; i < ennemyMaterial.Length; ++i)
        {
            ennemyMaterial[i].SetOverrideTag("RenderType", "Ennemy");
        }

        AiCamera.Render();

        for (int i = 0; i < ennemyMaterial.Length; ++i)
        {
            ennemyMaterial[i].SetOverrideTag("RenderType", "Opaque");
        }
    }



    public void SetPosition(Vector3 coords)
    {
        transform.position = coords;
    }



    public void SetPath(PathFinder pathFinder, int start, int end)
    {
        SetPath(pathFinder, start, end, false);
    }

    public void SetPath(PathFinder pathFinder, int start, int end, bool loop)
    {

        data.currentPath = pathFinder.FindPath(pathFinder.navPointStructure, start, end);
        data.currentPathIndex = 0;
        data.loopPath = loop;
        data.loopIndexModifier = 1;
        

    }
}
