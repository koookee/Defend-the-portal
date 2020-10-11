using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent agent;
    PlayerController player;
    private int health = 5;
    private float range = 2f;
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
        if (health <= 0) Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Not able to call range for some reason
        Gizmos.DrawWireSphere(transform.position, 7f);
    }
    private void MoveToPlayer()
    {
        float distance = Vector3.Distance(player.transform.position,transform.position);
        if(distance <= range)
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
