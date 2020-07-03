using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noise : MonoBehaviour {

    PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
        playerMovement = FindObjectOfType<PlayerMovement>();	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = playerMovement.transform.position;
    }
}
