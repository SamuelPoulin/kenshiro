using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour {

    public GameObject liam;
    public GameObject explosion;

    public Material[] liamMat;

    public Camera testCam;
    public Shader testShader;
    Camera cam;

    public GameObject navPointsContainer;

    public GameObject EnnemyPrefab;

    Vector3[] pointCoords;

    List<Transform> navPoints;
    

    const float AI_BASE_SPEED = 2f;
    PathFinder test;


    List<AiController> AiInstances;

    GameObject newAi;



    float timer;
    int xx;

    int detectionIndex;

    //System.Random generator;

    const int TEST_AI = 50;

    void Start () {

        cam = GetComponentInChildren<Camera>();
        detectionIndex = 0;

        AiInstances = new List<AiController>();

        InitializeNavPoints();
        test = new PathFinder(pointCoords);
        AddAi(TEST_AI);
        //AddAi(1);


        timer = 0;
        xx = 0;

        

        //generator = new System.Random();
    }
	

	void Update () {


        timer += Time.deltaTime;

        if (timer > 0.5 && xx < TEST_AI)
        {

            //AiInstances[xx].SetPath(test, 2, destination, true);
            ++xx;
            timer = 0;
        }
        /*
        if (xx == TEST_AI)
        {
            for (int i = 0; i < TEST_AI; ++i)
            {
                AiInstances[i].SetPath(test, AiInstances[i].data.currentPathIndex, destination);
            }
        }*/



        /*while(Vector3.Distance(AiInstances[detectionIndex].transform.position, liam.transform.position) > 300 && detectionIndex < AiInstances.Count - 1)
        {            
            detectionIndex++;
        }*/

        AiInstances[detectionIndex].UpdateCamera();


        for (int i = 0; i < AiInstances.Count; ++i)
        {
            if(AiInstances[i].data.currentPath == null)
            {
                int pos = AiInstances[i].data.currentDestinationIndex;
                int dest = Random.Range(0, 60);

                while (dest == pos)
                {
                    dest = Random.Range(0, 60);
                }

                AiInstances[i].SetPath(test, pos, dest);
            }


            if(Vector3.Distance(AiInstances[i].transform.position, liam.transform.position) < 20)
            {
                AiInstances[i].data.playerIsDetected = true;
            }


            if (AiInstances[i].data.playerIsDetected)
            {
                AiInstances[i].transform.LookAt(liam.transform);
            }
            else
            {
                AiInstances[i].transform.LookAt(AiInstances[i].data.currentDestination);
            }

            if (!AiInstances[i].data.wasSpotted && AiInstances[i].data.playerIsDetected)
            {
                /*Debug.Log(AiInstances[i].data.currentPath[AiInstances[i].data.currentPathIndex]);
                Debug.Log(AiInstances[i].data.currentPathIndex - 1 * AiInstances[i].data.loopIndexModifier);*/


                int pos = AiInstances[i].data.currentDestinationIndex;
                int dest = AiInstances[i].data.lastVisitedIndex;


                /*Debug.Log(pos);
                Debug.Log(dest);*/

                AiInstances[i].SetPath(test, pos, dest, true);
            }

            if (AiInstances[i].data.wasSpotted && !AiInstances[i].data.playerIsDetected)
            {
                int pos = AiInstances[i].data.currentDestinationIndex;
                int dest = Random.Range(0, 60);

                while (dest == pos)
                {
                    dest = Random.Range(0, 60);
                }
                /*Debug.Log(pos);
                Debug.Log(dest);*/

                AiInstances[i].SetPath(test, pos, dest);
            }

            if (AiInstances[i].data.IsDead())
            {
                Instantiate(explosion, AiInstances[i].transform.position, AiInstances[i].transform.rotation);
                Destroy(AiInstances[i].data.Arrow);
                Destroy(AiInstances[i].gameObject);
                AiInstances.RemoveAt(i);

                AddAi(1);
            }

            AiInstances[i].GetComponentInChildren<RectTransform>().LookAt(liam.transform);
        }

        

        detectionIndex++;
        detectionIndex = detectionIndex % AiInstances.Count;
        


        
    }


    void AddAi(int quantity)
    {
        for (int i = 0; i < quantity; ++i)
        {
            int pos = Random.Range(0, 60);
            int dest = Random.Range(0, 60);

            /*pos = 1;
            dest = 0;*/

            /*Debug.Log(pos);
            Debug.Log(dest);*/


            while (dest == pos)
            {
                dest = Random.Range(0, 60);
            }

            newAi = Instantiate(EnnemyPrefab);

            AiInstances.Add(newAi.GetComponent<AiController>());

            /*AiInstances[i].SetPosition(pointCoords[pos]);

            AiInstances[i].map = pointCoords;

            AiInstances[i].SetPath(test, pos, dest);

            AiInstances[i].data.lastVisitedIndex = pos;*/
            /*Debug.Log(pos);
            Debug.Log(dest);*/


            newAi.GetComponent<AiController>().SetPosition(pointCoords[pos]);

            newAi.GetComponent<AiController>().map = pointCoords;

            newAi.GetComponent<AiController>().SetPath(test, pos, dest);

            newAi.GetComponent<AiController>().data.lastVisitedIndex = pos;


            //AiInstances[i].ennemyMaterial = liamMat;

        }
    }

    void InitializeNavPoints()
    {
        navPoints = new List<Transform>();

        foreach (Transform t in navPointsContainer.transform)
        {
            navPoints.Add(t);
        }

        pointCoords = new Vector3[navPoints.Count];

        UpdateNavPointCoordinates();
    }

    void UpdateNavPointCoordinates()
    {
        for (int i = 0; i < pointCoords.Length; ++i)
        {
            pointCoords[i] = navPoints[i].transform.position + new Vector3(0, 2f, 0);
        }
    }

}
