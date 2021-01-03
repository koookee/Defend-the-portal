using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private GameManagerUI GameUI;
    public int health = 100;
    private int repulsiveForce = 20;
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
        GameObject[] enemyArr = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyArr)
        {
            EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
            Vector3 forceDirection = (enemyScript.transform.position - transform.position).normalized;
            //Only knocks back the enemies that are close to the portal
            if (Vector3.Distance(enemyScript.transform.position, transform.position) < 5f)
            {
                forceDirection.x *= repulsiveForce;
                forceDirection.z *= repulsiveForce;
                enemyScript.enemyRb.AddForce(forceDirection, ForceMode.Impulse);
                enemyScript.TakeDamage(damage);
            }
        }
        Player.numOfUranium--; //Ability uses up 1 uranium
        GameUI.numOfUranium.text = "Uranium: " + Player.numOfUranium;
    }
}
