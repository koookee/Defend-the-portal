using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round0 : MonoBehaviour
{
    private SpawnManager SpawnManagerScript;
    public int roundNum;
    private int roundDuration = 10; //Time the player has to scavenge resources
    private bool startNextRound = false;
    //If copying script, only change RoundX below and in update function
    private Round1 nextRound;

    // Start is called before the first frame update
    void Start()
    {
        SpawnManagerScript = GetComponent<SpawnManager>();
        roundNum = SpawnManagerScript.roundCounter;
        StartCoroutine("RoundTimer");
    }

    // Update is called once per frame
    void Update()
    {
        if (roundNum == SpawnManagerScript.roundCounter && startNextRound)
        {
            nextRound = GetComponent<Round1>();
            SpawnManagerScript.roundCounter++;
            nextRound.enabled = true;
            //Turns off script when round is over
            enabled = false;
        }
    }
    private IEnumerator RoundTimer()
    {
        yield return new WaitForSeconds(roundDuration);
        startNextRound = true;
    }
}
