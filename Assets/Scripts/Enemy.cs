using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] positions;
    public float speed;
    private int currentTarget = 0;
    public Transform player;
    public bool spottedPlayer = false;
    public float attackSpeed;
    private float attackCooldown;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.SetDestination(positions[currentTarget].position);
        attackCooldown = attackSpeed;
    }

    private void Update()
    {
        if(attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        if(agent.speed != speed)
            agent.speed = speed;
        if (spottedPlayer)
        {
            agent.SetDestination(player.position);
            if ((transform.position - agent.destination).magnitude < 2.0f && attackCooldown <= 0)
            {
                attackCooldown = attackSpeed;
                Attack();
            }
            if ((transform.position - agent.destination).magnitude > 5.0f)
            {
                spottedPlayer = false;
                agent.SetDestination(positions[currentTarget].position);
            }
        }

        else if((transform.position - agent.destination).magnitude < 1.1f)
        {
            currentTarget++;
            currentTarget = currentTarget % positions.Length;
            agent.SetDestination(positions[currentTarget].position);
        }

        
    }

    private void Attack()
    {
        Debug.Log("Rawr");
    }

    public void Notice(Transform t)
    {
        spottedPlayer = true;
        player = t;
    }

}