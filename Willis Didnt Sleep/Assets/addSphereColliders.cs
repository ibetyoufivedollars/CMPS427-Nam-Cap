using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addSphereColliders : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<SphereCollider>();
            child.gameObject.tag = "pellet";
            child.transform.localScale = new Vector3(.5f, .5f, .5f);
            //child.gameObject.SphereCollider.isTrigger = true;
            SphereCollider sphereCollider = child.GetComponent<SphereCollider>();
            sphereCollider.radius = .75f;
            sphereCollider.isTrigger = true;
            //child.transform.lossyScale;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
