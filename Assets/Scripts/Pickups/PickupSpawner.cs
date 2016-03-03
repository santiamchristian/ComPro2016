﻿using UnityEngine;
using System.Collections;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] Prefab;
    public GameObject SpawnedPickup;


    // Spawn Delay in seconds
    public float interval = 30;
    // Use this for initialization
    void Start ()
    {
        //if something to make it not spawn if one is already there
        InvokeRepeating("SpawnNext", interval, interval);
    }
	void SpawnNext(int i)
    {
        if(SpawnedPickup == null)
            SpawnedPickup = (GameObject)Instantiate(Prefab[i], transform.position, Quaternion.identity);
    }
    void Update()
    {
       
        int randomNumber = Random.Range(0, 3);
        //random number generator (0 between length of prefab)
        if (SpawnedPickup == null)
            SpawnNext(randomNumber);
    }
	
}
