using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpToBoss : MonoBehaviour {
    GameObject player;

    public AudioManager am;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.GetComponent<Movement>().superpowered)
            {
                player.transform.position = new Vector3(252.92f, 435.3f, 343.8791f);
                am.currentSong = 6;
                am.musicSource.Play();
            }
            else
            {
                player.transform.position = new Vector3(238.344f, .18f, 16.067f);//warp back to start
            }
        }
    }
}
