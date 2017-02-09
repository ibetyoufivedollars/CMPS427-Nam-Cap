using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
