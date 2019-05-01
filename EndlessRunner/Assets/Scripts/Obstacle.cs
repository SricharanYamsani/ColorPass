using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public Material mat;

    float speed = 6f;

    public bool instantiated = false;

	void Start () {


        mat = GetComponent<MeshRenderer>().material;
        mat.color = ObstacleSpawner.colorList[Random.Range(0,ObstacleSpawner.colorList.Count)];
	}
	
	void Update () {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if(instantiated)
        {
            instantiated = false;

            Invoke("BackToPool", 5f);
        }
	}

    void BackToPool()
    {
        Debug.Log("BTP");
        this.gameObject.SetActive(false);
        ObstacleSpawner.obstaclePool.Enqueue(this.gameObject);
    }
}
