using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public PlayerController PlayerControllerScript;
    public GameManager GameManagerScript;

    //Rounds:
    public int roundCounter = 1; //Increments as game progresses

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

        //Spawns enemies in this field of range
        for (int i = 0; i < amount; i++)
        {
            //Spawns an int amount of enemy prefabs 
            float enemyRangeX = UnityEngine.Random.Range(-30, 10); //Adjust boundary later on
            float enemyRangeZ = UnityEngine.Random.Range(36, 45); //Adjust noundary later on
            Vector3 enemySpawnLocation = new Vector3(enemyRangeX, 2, enemyRangeZ);
            Instantiate(prefabs[enemyType], enemySpawnLocation, prefabs[enemyType].transform.rotation);
        }
    }
}
