using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleSpawner : MonoBehaviour {

    public static List<Color> colorList;

    public GameObject obstaclePrefab;
    public static Queue<GameObject> obstaclePool = new Queue<GameObject>();

    float Offset = 15f;

    float TimeForNextSpawn;
    float SpawnRate = 0.25f;

    private void Awake()
    {
        colorList = new List<Color>();

        AddColors();

        Init();
    }
    void Start () {
        
        TimeForNextSpawn = 0f;
         

    }

    void Update () {
        Spawn();
	}

    void Spawn()
    {
        if(Time.time > TimeForNextSpawn)
        {
            TimeForNextSpawn = Time.time + SpawnRate;
            GameObject obstacle = obstaclePool.Dequeue();
            obstacle.SetActive(true);
            obstacle.GetComponent<Obstacle>().instantiated = true;
            obstacle.transform.position = new Vector3(Random.Range(10f,-10f),0.5f, transform.position.z + Offset);
        }
    }

    private void Init()
    {
        for(int i = 0; i < 50; i++)
        {
            GameObject obstacle = Instantiate(obstaclePrefab, transform);
            obstacle.SetActive(false);
            obstaclePool.Enqueue(obstacle);
        }
    }

    void AddColors()
    {
        colorList.Add(Color.black);
        colorList.Add(Color.blue);
        colorList.Add(Color.red);
        colorList.Add(Color.green);
       // colorList.Add(Color.yellow);
       // colorList.Add(Color.cyan);
    }
}
