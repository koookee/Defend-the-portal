using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent agent;
    private PlayerController player;
    private PortalScript portal;
    private GameManager GameManagerScript;
    public Rigidbody enemyRb;
    private int health = 5;
    private float detectionRange = 999f; //Detection range is the entire map
    private float attackRange; //attack range is dependant on the stopping distance
    private bool isAlive = true;
    private float attackSpeed = 1f; //1 attacks per second
    private float attackCooldown = 0f; //Updates with Time.time
    private bool shouldTargetPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        portal = GameObject.Find("Portal").GetComponent<PortalScript>();
        GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        attackRange = agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        MoveToTarget();
        ShouldTargetPlayer();
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
            //If enemy is currently targeting player and they die, it decrements number of enemies targeting player
            if (shouldTargetPlayer) GameManagerScript.numEnemiesTargetingPlayer--; 
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

            if (distanceToPlayer <= detectionRange && distanceToPlayer >= attackRange && shouldTargetPlayer)
            {
                //Moves enemy to player
                //Reason as to why I added isAlive is so that it doesn't execute after the enemy is dead
                //which causes a bug
                agent.SetDestination(player.transform.position);
            }
            else if (distanceToPlayer <= attackRange && shouldTargetPlayer)
            {
                //If enemy is in combat range, they look at the player
                LookAtPlayer();
                Attack(attackSpeed,"Player");
                //Knocks the player back when hit
                Vector3 knockbackDirection = player.transform.position - transform.position;
                knockbackDirection = new Vector3(knockbackDirection.x * 1.5f, 2f, knockbackDirection.z * 1.5f); //To throw the player into the air
                player.playerRB.AddForce(knockbackDirection, ForceMode.Impulse);
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
        }

    }
    private void ShouldTargetPlayer()
        //Checks if the enemy should target the player or go straight to the portal
        //If the maximum number of enemies targeting player hasn't been reached, the
        //current enemy will target the player
        //Also checks if the enemy is currently targeting the player
    {
        if(GameManagerScript.numEnemiesTargetingPlayer < GameManagerScript.maxNumEnemiesTargetingPlayer && !shouldTargetPlayer)
        {
            shouldTargetPlayer = true;
            GameManagerScript.numEnemiesTargetingPlayer++;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
