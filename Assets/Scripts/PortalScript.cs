using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private GameManagerUI GameUI;
    public int health = 100;
    private int repulsiveForce = 200;
    private int paralyzingTime = 5; // Time in seconds
    private int damage = 1; //Damage applied by the repulsive force
    PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        GameUI = GameObject.Find("GameManagerUI").GetComponent<GameManagerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool PlayerInRange()
    {
        if(Vector3.Distance(Player.transform.position, transform.position) < 4)
        {
            return true;
        }
        return false;
    }
    public void RepelEnemies()
    {
        bool repelledEnemy = false;
        GameObject[] enemyArr = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyArr)
        {
            EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
            Vector3 forceDirection = (enemyScript.transform.position - transform.position).normalized;
            //Only knocks back the enemies that are close to the portal
            if (Vector3.Distance(enemyScript.transform.position, transform.position) < 5f)
            {
                repelledEnemy = true;
                enemyScript.enemyRb.isKinematic = false; //Remember to turn this back on
                forceDirection.x *= repulsiveForce;
                forceDirection.z *= repulsiveForce;
                enemyScript.enemyRb.AddForce(forceDirection, ForceMode.Impulse);
                enemyScript.TakeDamage(damage);
                //Checks if the enemy is still alive
                if(enemyScript.health>0) StartCoroutine(ParalyzeEnemy(enemyScript, enemyScript.agent.speed));
            }
        }
        if (repelledEnemy) //If no enemies were repelled, uranium is not used up
        {
            Player.numOfUranium--; //Ability uses up 1 uranium
            GameUI.numOfUranium.text = "Uranium: " + Player.numOfUranium;
        }
    }
    private IEnumerator ParalyzeEnemy(EnemyScript enemy, float originalSpeed)
    {
        enemy.agent.speed = 0;
        yield return new WaitForSeconds(paralyzingTime);
        if (enemy.health > 0)
        {
            enemy.agent.speed = originalSpeed;
            enemy.enemyRb.isKinematic = true;
        }
    }
}
