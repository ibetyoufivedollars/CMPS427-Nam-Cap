using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyStates {
    private NavMeshAgent agent;
    private int destination;
    private readonly EnemyScript enemy;
    public Transform player;

    public ChaseState(EnemyScript thisEnemy)
    {
        enemy = thisEnemy;
        agent = enemy.GetComponent<NavMeshAgent>();
    }

    public void OnCollisionEnter(Collision other)
    {
        //Take damage or deal damage
    }

    public void OnTriggerEnter(Collider other)
    {
        //Nothing
    }

    public void ToSearchState()
    {
        Debug.Log("Entering search state.");
        enemy.currentState = enemy.searchState;
    }

    public void TakeDamage()
    {
        //Damage taken
    }

    public void ToChaseState()
    {
        Debug.Log("Can't change state, already in chase state.");
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void UpdateState()
    {
        Look();
        Chase();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Look()
    {
        RaycastHit hit;
        Vector3 enemyToTarget = (enemy.target.position + enemy.offset) - enemy.eyes.transform.position;
        if(Physics.Raycast(enemy.eyes.transform.position, enemyToTarget, out hit, enemy.eyeSight) && hit.collider.CompareTag("Player"))
        {
            enemy.target = hit.transform;
        } else
        {
            ToSearchState();
        }
    }

    void Chase()
    {
        enemy.navMeshAgent.destination = enemy.target.position;
        enemy.navMeshAgent.Resume();
    }
}
