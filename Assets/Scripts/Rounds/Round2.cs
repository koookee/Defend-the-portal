using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round2 : MonoBehaviour
{
    private SpawnManager SpawnManagerScript;
    public int timeBeforeRoundStarts = 5;
    private bool allEnemiesSpawned = false;
    public int roundNum;
    //If copying script, only change RoundX below and in update function
    private Round2 nextRound; //Change to round 3

    // Start is called before the first frame update
    void Start()
    {
        SpawnManagerScript = GetComponent<SpawnManager>();
        roundNum = SpawnManagerScript.roundCounter;
        StartCoroutine("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (allEnemiesSpawned)
        {
            //Makes an array to see how many enemies are left
            GameObject[] enemyArr = GameObject.FindGameObjectsWithTag("Enemy");
            //Stops the if condition from being executed if the roundCounter gets incremented
            if (enemyArr.Length == 0 && roundNum == SpawnManagerScript.roundCounter)
            {
                nextRound = GetComponent<Round2>(); //Change to round 3
                SpawnManagerScript.roundCounter++;
                nextRound.enabled = true;
                //Turns off script when round is over
                enabled = false;
            }
        }
    }
    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(timeBeforeRoundStarts);

        int enemy1 = 0;
        /*
        SpawnManagerScript.EnemySpawner(enemy1, 1);
        yield return new WaitForSeconds(10);
        SpawnManagerScript.EnemySpawner(enemy1, 10);
        yield return new WaitForSeconds(20);
        SpawnManagerScript.EnemySpawner(enemy1, 3);
        yield return new WaitForSeconds(20);
        SpawnManagerScript.EnemySpawner(enemy1, 3);
        */

        allEnemiesSpawned = true;
    }
}
