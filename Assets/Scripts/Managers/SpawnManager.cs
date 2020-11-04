using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public PlayerController PlayerControllerScript;
    public GameManager GameManagerScript;

    //Rounds:
    public int roundCounter = 0; //Increments as game progresses

    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Gets called by the round scripts
    public void EnemySpawner(int enemyType, int amount)
    {
        //enemyType determines what enemy to spawn
        // prefabs[0] is regular (enemy1)

        for (int i = 0; i < amount; i++) //Spawns an int amount of enemy prefabs 
        {
            Vector3 enemySpawnLocation = CalculateSpawnRange(new Vector2(-20,20), new Vector2(-20, 20), new Vector2(-50, 50), new Vector2(-50, 50));  
            Instantiate(prefabs[enemyType], enemySpawnLocation, prefabs[enemyType].transform.rotation);
        }
    }
    private Vector3 CalculateSpawnRange(Vector2 innerSquareX, Vector2 innerSquareZ, Vector2 outerSquareX, Vector2 outerSquareZ)
    //Returns the spawn area of the enemies. Think of a small square inside of a larger square.
    //The enemies can spawn anywhere in the outer square but the area inside the smaller square.
    //This prevents the enemies from spawning directly in front of the portal.
    {
        float area1X = UnityEngine.Random.Range(innerSquareX.x,innerSquareX.y);
        float area1Z = UnityEngine.Random.Range(innerSquareZ.y, outerSquareZ.y); //top border
        float area2X = UnityEngine.Random.Range(outerSquareX.x, innerSquareX.x);
        float area2Z = UnityEngine.Random.Range(outerSquareZ.x, outerSquareZ.y); //left border 
        float area3X = UnityEngine.Random.Range(innerSquareX.y, outerSquareX.y);
        float area3Z = UnityEngine.Random.Range(outerSquareZ.x, outerSquareZ.y); //right border
        float area4X = UnityEngine.Random.Range(innerSquareX.x, innerSquareX.y);
        float area4Z = UnityEngine.Random.Range(outerSquareZ.x, innerSquareZ.x); //bottom border
        Vector2 area1 = new Vector2(area1X, area1Z);
        Vector2 area2 = new Vector2(area2X, area2Z);
        Vector2 area3 = new Vector2(area3X, area3Z);
        Vector2 area4 = new Vector2(area4X, area4Z);
        Vector3[] areaArr = { area1, area2, area3, area4 };
        int num = UnityEngine.Random.Range(0, 4); // Picks a random area
        float enemyRangeX = areaArr[num].x;
        float enemyRangeZ = areaArr[num].y;
        Vector3 spawnArea = new Vector3(enemyRangeX, 2, enemyRangeZ);
        return spawnArea;
    }
}
