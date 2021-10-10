using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObject : MonoBehaviour
{

    public GameObject spawnPrefab;

    [SerializeField] public float minSecondsBetweenSpawning = 6.0f;
    [SerializeField] public float maxSecondsBetweenSpawning = 12.0f;
    private float savedTime;
    private float secondsBetweenSpawning;


    // Start is called before the first frame update
    void Start()
    {
        savedTime = Time.time;
        secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - savedTime >= secondsBetweenSpawning) // is it time to spawn again?
        {
            MakeThingToSpawn();
            savedTime = Time.time; // store for next spawn
            secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
        }
    }

    void MakeThingToSpawn()
    {
        // create a new gameObject
        GameObject clone = Instantiate(spawnPrefab, transform.position, spawnPrefab.transform.rotation) as GameObject;
    }
}
