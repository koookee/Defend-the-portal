using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent agent;
    PlayerController player;
    private int health = 5;
    private float detectionRange = 7f;
    private float attackRange = 1.5f;
    private bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        MoveToPlayer();
    }
    public void TakeDamage(int damageTook)
    {
        health -= damageTook;
    }
    private void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            isAlive = false;
        }
    }
    
    private void MoveToPlayer()
    {
        float distance = Vector3.Distance(player.transform.position,transform.position);
        if (distance <= detectionRange && distance >= attackRange && isAlive)
        {
            //Moves enemy to player
            //Reason as to why I added isAlive is so that it doesn't execute after the enemy is dead
            //which causes a bug
            agent.SetDestination(player.transform.position);
        }
        else
        {
            //If enemy is in combat range, they look at the player
            LookAtPlayer();
        }
    }
    private void LookAtPlayer()
    {
        //Enemy looks at player when they are in combat range
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
