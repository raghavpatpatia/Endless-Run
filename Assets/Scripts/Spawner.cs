using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPos = new Vector3(35.0f, 0, 0);
    private float startTime = 1.5f;
    private PlayerController playerControllerScript;
    private int randomObstacle;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("spawn", startTime, Random.Range(2.0f, 4.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn()
    {
        if (playerControllerScript.isGameOver == false)
        {
            randomObstacle = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[randomObstacle], spawnPos, obstaclePrefabs[randomObstacle].transform.rotation);
        }
    }
}
