using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IEnemyStates {

    //public list of waypoints for patrolling
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private int destination;
    private readonly EnemyScript enemy;

    public PatrolState(EnemyScript thisEnemy)
    {
        enemy = thisEnemy;
    }
	
	// Update is called once per frame
	public void UpdateState () {
        Look();
        Patrol();
	}

    void Patrol()
    {
        enemy.navMeshAgent.destination = enemy.waypoints[destination].position;
        enemy.navMeshAgent.Resume();

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            destination = (destination + 1) % enemy.waypoints.Length;

        }
    }

    void Look()
    {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.eyeSight) && hit.collider.CompareTag("Player"))
        {
            enemy.target = hit.transform;
            ToChaseState();
        }
    }

    /*
    void GoToNextWayPoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.SetDestination(waypoints[destination].position);

        destination = (destination + 1) % waypoints.Length;
    }*/

    public void ToPatrolState()
    {
        Debug.Log("Can't change to same state");
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
        Debug.Log("Starting pursuit of Pac-Man");
    }

    public void ToSearchState()
    {
        enemy.currentState = enemy.patrolState;
        Debug.Log("");
    }

    //mostly for animation
    public void TakeDamage()
    {
        Debug.Log("Damage taken.");
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entering search state");
            ToSearchState();
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //Damage player or take damage
        }
    }
}
