using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiData {

    private int _currentHp;

    public int[] currentPath;

    public int currentPathIndex;

    public bool loopPath;

    public int loopIndexModifier;

    public bool playerIsDetected;

    public int currentDestinationIndex;
    public Vector3 currentDestination;
    public Vector3 travelDirection;

    public static Vector3 height = new Vector3(0, 2.11f, 0);

    float uncertainty = 0.5f;

    public GameObject Arrow;

    public bool wasSpotted;

    public int lastVisitedIndex;


    public float spotting = 0;

    public int health = 100;
    
    int CurrentHp
    {
        get
        {
            return _currentHp;
        }

        set
        {
            _currentHp += value;
            if (_currentHp < 0)
                _currentHp = 0;
        }
    }



    public bool IsAtCurrentDestination(Vector3 position)
    {
        return (position.x > currentDestination.x - uncertainty && position.x < currentDestination.x + uncertainty && position.z > currentDestination.z - uncertainty && position.z < currentDestination.z + uncertainty);
    }




    public AiData()
    {
        playerIsDetected = false;
    }



    public void TakeDamage(int damage)
    {
        health -= damage;

    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
