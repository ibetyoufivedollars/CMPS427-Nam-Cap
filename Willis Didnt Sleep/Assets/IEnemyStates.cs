using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStates {

    void UpdateState();

    void ToPatrolState();

    void ToChaseState();

    void ToSearchState();

    void TakeDamage();

    void OnTriggerEnter(Collider other);

    void OnCollisionEnter(Collision other);
}
