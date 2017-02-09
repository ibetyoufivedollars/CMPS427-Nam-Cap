using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : IEnemyStates
{
    private readonly EnemyScript enemy;
    private float searchTimer;

    public SearchState(EnemyScript thisEnemy)
    {
        enemy = thisEnemy;
    }

    public void UpdateState()
    {
        Look();
        Search();
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void ToPatrolState()
    {
        Debug.Log("Resuming Patrol");
        enemy.currentState = enemy.patrolState;
        searchTimer = 0f;
    }

    public void ToAlertState()
    {
        Debug.Log("Can't transition to same state");
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
        searchTimer = 0f;
    }

    private void Look()
    {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.eyeSight) && hit.collider.CompareTag("Player"))
        {
            enemy.target = hit.transform;
            ToChaseState();
        }
    }

    private void Search()
    {
        enemy.navMeshAgent.Stop();
        enemy.transform.Rotate(0, enemy.searchTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.searchDuration)
            ToPatrolState();
    }

    public void ToSearchState()
    {
        Debug.Log("Already in search state.");
    }

    public void TakeDamage()
    {
        //Take damage
    }

    public void OnCollisionEnter(Collision other)
    {
        //Nothing
    }
}
