using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private PlayerController player;
    private PortalScript portal;
    private int health = 5;
    private float detectionRange = 7f;
    private float attackRange; //attack range is dependant on the stopping distance
    private bool isAlive = true;
    private float attackSpeed = 1f; //1 attacks per second
    private float attackCooldown = 0f; //Updates with Time.time

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        portal = GameObject.Find("Portal").GetComponent<PortalScript>();
        attackRange = agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        MoveToTarget();
    }
    public void TakeDamage(int damageTook)
    {
        health -= damageTook;
        Debug.Log("Enemy health: " + health);
    }
    private void CheckHealth()
    {
        if (health <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }

    private void MoveToTarget()
    {
        //Update method gets called after enemy is destroyed
        //The if condition prevents the method from executing if the enemy is destroyed
        if (isAlive)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            float distanceToPortal = Vector3.Distance(portal.transform.position, transform.position);

            if (distanceToPlayer <= detectionRange && distanceToPlayer >= attackRange)
            {
                //Moves enemy to player
                //Reason as to why I added isAlive is so that it doesn't execute after the enemy is dead
                //which causes a bug
                agent.SetDestination(player.transform.position);
            }
            else if (distanceToPlayer <= attackRange)
            {
                //If enemy is in combat range, they look at the player
                LookAtPlayer();
                Attack(attackSpeed,"Player");
            }
            else
            {
                //If player is not in range, enemy will target portal
                agent.SetDestination(portal.transform.position);
                if (distanceToPortal <= attackRange)
                {
                    Attack(attackSpeed,"Portal");
                }
            }
        }
    }
    private void LookAtPlayer()
    {
        //Enemy looks at player when they are in combat range
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void Attack(float attackSpeed, string objectToAttack)
    {
        //Play attack animation
        //Wait until animation is over to do damage
        if (Time.time > attackCooldown)
        {
            if (objectToAttack == "Player") player.health--;
            else portal.health--;
            attackCooldown = Time.time + 1 / attackSpeed;
            Debug.Log("Player health: " + player.health);
            Debug.Log("Portal health: " + portal.health);
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
