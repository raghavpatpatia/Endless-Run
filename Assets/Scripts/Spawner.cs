using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] Transform spawnPos;
        // = new Vector3(35.0f, 0, 0);
    private float startTime = 1.5f;
    [SerializeField] PlayerController playerControllerScript;
    private int randomObstacle;
    private int randomSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        randomSpawnTime = Random.Range(2, 4);
        InvokeRepeating("spawn", startTime, randomSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        randomSpawnTime = Random.Range(2, 4);
    }

    void spawn()
    {
        if (!playerControllerScript.isGameOver)
        {
            randomObstacle = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[randomObstacle], spawnPos.position, obstaclePrefabs[randomObstacle].transform.rotation);
        }
    }
}
