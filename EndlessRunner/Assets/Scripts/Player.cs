using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Material mat;

    int colorIndex = -1;

    Color prevColor;

	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        mat.color = Color.black;
        prevColor = Color.white;
        // ChangeColorUp();
	}
	
	void Update () {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"),0,0);
        transform.Translate(dir * 5f * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.K))
            ChangeColorUp();

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L))
            ChangeColorDown();
	}

    void ChangeColorUp()
    {
        if (colorIndex == ObstacleSpawner.colorList.Count - 1)
            colorIndex = -1;
        mat.color = ObstacleSpawner.colorList[++colorIndex];
    }

    void ChangeColorDown()
    {
        if (colorIndex == 0)
            colorIndex = ObstacleSpawner.colorList.Count;
        mat.color = ObstacleSpawner.colorList[--colorIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            //Debug.Log("Obstacle");
            if (mat.color == other.gameObject.GetComponent<Obstacle>().mat.color)
            {
                Debug.Log("Same color");



                GameManager.Instance.UpdateScore(1, prevColor == mat.color);
               // GameManager.Instance.CheckStreak(prevColor == mat.color);

                prevColor = mat.color;
            }
            else
            {
                Debug.Log("Obstacle");
                GameManager.Instance.GameOver();
            }
        }
    }
}
