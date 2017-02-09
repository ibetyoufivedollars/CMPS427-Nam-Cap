using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {
    public Transform[] waypoints;
    public float speed;
    public float turningSpeed;
    public Transform eyes;
    public float eyeSight = 25f;
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public float searchDuration = 3f;
    public float searchTurnSpeed = 100f;

    public Transform target;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public IEnemyStates currentState;
    [HideInInspector]
    public SearchState searchState;
    
    private void Awake ()
    {
        searchState = new SearchState(this);
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    // Use this for initialization
    void Start () {
        currentState = patrolState;
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState();
	}

    void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
}
